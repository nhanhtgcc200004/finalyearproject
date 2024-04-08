using finalyearproject.Models;
using finalyearproject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace finalyearproject.Controllers
{
    public class ReviewController : Controller
    {
        private ApplicationDBcontext _dbContext;
        private ReviewRepo _reviewRepo;
        public ReviewController(ApplicationDBcontext dbContext, ReviewRepo reviewRepo)
        {
            _dbContext = dbContext;
            _reviewRepo = new ReviewRepo(_dbContext);
        }


        public async Task<IActionResult> Index()
        {
            List<Review> reviews = await _reviewRepo.GetAllReviews();
            return View(reviews);
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
