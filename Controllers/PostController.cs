using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase 
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostRepository _PostRepository;

        public PostController(ILogger<PostController> logger, IPostRepository repository)
        {
            _logger = logger;
            _PostRepository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetPost() 
        {
            return Ok(_PostRepository.GetAllPost());
        }

        [HttpGet]
        [Route("{PostId:int}")]
        public ActionResult<Post> GetPostById(int postId) 
        {
            var Post = _PostRepository.GetPostById(postId);
            if (Post == null) {
                return NotFound();
            }
            return Ok(Post);
        }
        [HttpGet]
        [Route("{username}")]
        public ActionResult<Post> GetUsersPosts(string username) 
        {
            var Post = _PostRepository.GetUsersPosts(username);
            if (Post == null) {
                return NotFound();
            }
            return Ok(Post);
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Post> CreatePost(Post Post) 
        {
            if (!ModelState.IsValid || Post == null) {
                return BadRequest();
            }
            var newPost = _PostRepository.CreatePost(Post);
            return Created(nameof(GetPostById), newPost);
        }

        [HttpPut]
        [Route("{PostId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Post> UpdatePost(Post Post) 
        {
            if (!ModelState.IsValid || Post == null) {
                return BadRequest();
            }
            return Ok(_PostRepository.UpdatePost(Post));
        }

        [HttpDelete]
        [Route("{PostId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult DeletePost(int PostId) 
        {
            _PostRepository.DeletePostById(PostId); 
            return NoContent();
        }
        
    }
}