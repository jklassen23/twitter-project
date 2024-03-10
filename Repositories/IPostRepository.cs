using System.Collections.Generic;
using backend.Models;

namespace backend.Repositories;
public interface IPostRepository
{
    IEnumerable<Post> GetAllPost();
    Post? GetPostById(int PostId);
    Post CreatePost(Post newPost);
    Post? UpdatePost(Post newPost);
    void DeletePostById(int PostId);
    IEnumerable<Post>? GetUsersPosts(string username);
}