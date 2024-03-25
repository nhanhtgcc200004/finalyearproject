using finalyearproject.Models;
using Microsoft.EntityFrameworkCore;

namespace finalyearproject.Repositories
{
    public class PostRepo
    {
        private ApplicationDBcontext dbcontext;

        public PostRepo(ApplicationDBcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<List<Post>> SearchAllPost()
        {
            return await dbcontext.Posts.ToListAsync();
        }
        public async Task<Post> SearchPostById(int post_id)
        {
            return await dbcontext.Posts.Where(p=>p.post_id==post_id).FirstOrDefaultAsync();
        }
        public async Task<List<Post>> SearchPost(string search_value)
        {
            return await dbcontext.Posts.Where(p=>p.post_title.Contains(search_value)).ToListAsync();
        }
    }
}
