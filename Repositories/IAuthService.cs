using backend.Models;

namespace backend.Repositories;

public interface IAuthService 
{
    User CreateUser(User user);
    string SignIn(string Username, string Password);
    public User? GetUserByUsername(string username);
    User? UpdateUser(User newUser);
}
