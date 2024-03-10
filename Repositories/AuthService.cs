using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Migrations;
using backend.Models;
using Microsoft.IdentityModel.Tokens;
using bcrypt = BCrypt.Net.BCrypt;

namespace backend.Repositories;

public class AuthService : IAuthService
{
    private static PostDbContext _context;
    private static IConfiguration _config;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(PostDbContext context, IConfiguration config, IHttpContextAccessor httpContextAccessor) {
        _context = context;
        _config = config;
        _httpContextAccessor = httpContextAccessor;
    }

    public User CreateUser(User user)
    {
        var passwordHash = bcrypt.HashPassword(user.Password);
        user.Password = passwordHash;
        
        _context.Add(user);
        _context.SaveChanges();
        return user;
    }

    public string SignIn(string Username, string Password)
    {
        var user = _context.Users.SingleOrDefault(x => x.Username == Username);
        var verified = false;

        if (user != null) {
            verified = bcrypt.Verify(Password, user.Password);
        }

        if (user == null || !verified)
        {
            return String.Empty;
        }
        
        return BuildToken(user);
    }

    private string BuildToken(User user) {
        var secret = _config.GetValue<String>("TokenSecret");
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim("User_userid", user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username ?? ""),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName ?? ""),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName ?? "")
        };

        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: signingCredentials);

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }

    public User? GetUserByUsername(string username){
        return _context.Users.FirstOrDefault(u => u.Username == username);
    }
    public User? UpdateUser(User newUser)
    {
        var originalUser = _context.Users.FirstOrDefault(u => u.Username == newUser.Username);
        if (originalUser != null)
        {
            originalUser.Username = newUser.Username;
            originalUser.FirstName = newUser.FirstName;
            originalUser.LastName = newUser.LastName;
            originalUser.Bio = newUser.Bio;
            originalUser.Country = newUser.Country;
            _context.SaveChanges();
        }
        return originalUser;
    }
}