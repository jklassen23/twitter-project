using backend.Migrations;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class PostRepository : IPostRepository 
{
    private readonly PostDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PostRepository(PostDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public Post CreatePost(Post newPost)
{
    var username = _httpContextAccessor.HttpContext.User.Identity.Name;
    var user = _context.Users.SingleOrDefault(u => u.Username == username);
    

    if (user != null)
    {

        newPost.Username = username;
        newPost.DateTime = DateTime.Now;
        newPost.UserId = user.UserId;
        newPost.User = user;

        _context.Post.Add(newPost);
        _context.SaveChanges();

        return newPost;
    }

    throw new InvalidOperationException("User not found");
}

    public void DeletePostById(int PostId)
    {
        var Post = _context.Post.Find(PostId);
        if (Post != null) {
            _context.Post.Remove(Post); 
            _context.SaveChanges(); 
        }
    }

    public IEnumerable<Post> GetAllPost()
    {
         return _context.Post.ToList();
    }

    public Post? GetPostById(int PostId)
    {
        return _context.Post.SingleOrDefault(c => c.PostId == PostId);
    }
    public IEnumerable<Post> GetUsersPosts(string username)
    {
        return _context.Post.Where(post => post.Username == username).ToList();
    }

    public Post? UpdatePost(Post newPost)
    {
        var originalPost = _context.Post.Find(newPost.PostId);
        if (originalPost != null) {
            originalPost.Username = newPost.Username;
            originalPost.Content = newPost.Content;
            originalPost.DateTime = newPost.DateTime;
            _context.SaveChanges();
        }
        return originalPost;
    }
}