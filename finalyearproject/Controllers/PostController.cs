using finalyearproject.Models;
using finalyearproject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace finalyearproject.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDBcontext _dbContext;
        private readonly UserRepo userRepo;
        private PostRepo postRepo;
        private ISession Session;
        private int user_id;
        private string role;
        public PostController(ApplicationDBcontext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            userRepo = new UserRepo(dbContext);
            postRepo = new PostRepo(dbContext);
            Session = httpContextAccessor.HttpContext.Session;
            user_id = (int) Session.GetInt32("user_id");
            role = Session.GetString("role");
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int post_id)
        {
            if (CheckUserInfo())
            {
                Post post = await postRepo.SearchPostById(post_id);
                return View(post);
            }
            else return NotFound();
        }
        public IActionResult CreatePost()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreatePost([FromForm] Post post)
        {
            if (CheckUserInfo())
            {
                HandleCreatePost(post);
                return View();
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> UpdatePost(int post_id)
        {
            Post post=await postRepo.SearchPostById(post_id);
            if (post != null&& user_id!=null && user_id==post.user_id)
            {
                return View();
            }
            else { return NotFound(); }
        }
        public async Task<IActionResult> UpdatePost([FromForm] Post post)
        {
            HandleUpdatePost(post);
            return RedirectToAction("Detail","Post", new {post_id=post.post_id});
        }

        private void HandleUpdatePost(Post post)
        {
            _dbContext.Update(post);
            _dbContext.SaveChanges();
        }

        private bool CheckUserInfo()
        {
           if(user_id!=null&& role != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void HandleCreatePost(Post post)
        {
            _dbContext.Add(post);
            _dbContext.SaveChanges();
        }
       
        [HttpPost]
        public void DeletePost(int id)
        {
            throw new NotImplementedException();
        }
    }
}
