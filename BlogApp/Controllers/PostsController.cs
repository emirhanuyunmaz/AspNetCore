using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;

        private readonly ICommentRepository _commentRepository;
        public PostsController(IPostRepository postRepository,ITagRepository tagRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            _commentRepository = commentRepository;
        }
        public async Task<IActionResult> Index(string? tag){
            
            var claims = User.Claims;

            var posts = _postRepository.Posts;

            if(!string.IsNullOrEmpty(tag)){
                posts = posts.Where(x => x.Tags.Any(t => t.Url == tag)); 
            }

            return View(
                new PostsViewModel{Posts = await posts.ToListAsync(),}
            );
        }
        public async Task<IActionResult> Details(string? url){
            return View(await _postRepository.Posts.Include(x => x.Tags).Include(x => x.Comments).ThenInclude(x => x.User).FirstOrDefaultAsync(p => p.Url == url));
        }

        [HttpPost]
        public JsonResult AddComment(int PostId ,string Text){
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var avatar = User.FindFirstValue(ClaimTypes.UserData);

            var comment = new Comment{
                Text=Text,
                PublishedOn= DateTime.Now,
                PostId=PostId,
                UserId = int.Parse(userId ?? "")
            };
            _commentRepository.CreateComment(comment);
            
            // return Redirect("/posts/details"+Url);
            // return RedirectToRoute("post_details",new {url=Url});
            return Json(new {userName,Text,comment.PublishedOn,avatar});
        }

        [Authorize]
        public IActionResult Create(){
            
            return View();
        
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(PostCreateViewModel model){

            
            if(ModelState.IsValid){
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _postRepository.CreatePost(
                    new Post {
                        Title = model.Title,
                        Content = model.Content,
                        Url = model.Url,
                        UserId = int.Parse(userId ?? ""),
                        PublishedOn = DateTime.Now,
                        Image = "1.jpg",
                        IsActive = false
                    }
                );
                return RedirectToAction("Index");
            }
            
            return View(model);
        
        }

    }
}