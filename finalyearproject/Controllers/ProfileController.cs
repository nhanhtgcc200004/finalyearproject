using finalyearproject.Models;
using finalyearproject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace finalyearproject.Controllers
{
    public class ProfileController : Controller
    {
        private ISession session;
        private UserRepo _userRepo;
        private ApplicationDBcontext _dbcontext;
        private int user_id;
        private string role;
        public ProfileController(ApplicationDBcontext dBcontext, HttpContextAccessor httpContextAccessor)
        {
            _dbcontext = dBcontext;
            _userRepo = new UserRepo(_dbcontext);
            session =httpContextAccessor.HttpContext.Session;
            user_id =(int) session.GetInt32("user_id");
            role = session.GetString("role");
        }
        public IActionResult Index(int id)
        {
            if (Checkinfor(id))
            {
                return View();
            }
            return BadRequest();
        }
        public async Task<IActionResult> UpdateProfile(int id)
        {
            if (Checkinfor(id))
            {
                User user= await _userRepo.SearchUserById(id);
                
                return View(user);
            }
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProfile(int id, [FromForm] User user)
        {
            if (Checkinfor(id))
            {
                HandleUpdateProfile(user);
                return RedirectToAction("Index","Profile",id=id);
            }
            return BadRequest();
        }
        //[httpPost]
        //private void DownloadUserCv(User user)
        //{
        //        List<string> titles = new List<string>();
        //        List<CV> lst_selected_article = JsonConvert.DeserializeObject<List<DownLoadArticle>>(arrArticle);
        //        int user_id = (int)session.GetInt32("User_id");
        //        string role = session.GetString("role");
        //        if (user_id != null && role != "admin")
        //        {
        //            if (lst_selected_article.Count > 0)
        //            {
        //                List<MemoryStream> memories = new List<MemoryStream>();

        //                foreach (DownLoadArticle article in lst_selected_article)
        //                {
        //                    List<Article_file> lis_files = await _repoArticle_File.SearhAllArticleFileById(article.id);

        //                    MemoryStream memory = mailSystem.DownloadSingleFile(lis_files);
        //                    memories.Add(memory);
        //                    titles.Add(article.title);
        //                }
        //                if (memories.Count > 1)
        //                {
        //                    MemoryStream memori = await mailSystem.DownloadProcessAsync(memories, titles);
        //                    return File(memori.ToArray(), "application/zip", "selected_article.zip");

        //                }
        //                else if (memories.Count == 1)
        //                {
        //                    return File(memories.First().ToArray(), "application/zip", "selected_article.zip");
        //                }
        //            }
        //            return RedirectToAction("Index", "Article");
        //        }
        //        else
        //        {

        //            return RedirectToAction("NotFound", "Home");
        //        }
        //}
        private void HandleUpdateProfile(User user)
        {
            _dbcontext.Update(user);
            _dbcontext.SaveChanges();
        }

        private bool Checkinfor(int id)
        {
            if (user_id != null && user_id == id)
            {
                return true;
            }
            return false;
        }
    }
}
