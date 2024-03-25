﻿using finalyearproject.Models;
using Microsoft.EntityFrameworkCore;

namespace finalyearproject.Repositories
{
    public class CommentRepo
    {
        private ApplicationDBcontext _dbcontext;
        public CommentRepo(ApplicationDBcontext dBcontext)
        {
            _dbcontext = dBcontext;
        }
        public async Task<List<Comment>> GetAllCommentByPostId(int postId)
        {
            return await _dbcontext.Comments.Where(c => c.post_id == postId).ToListAsync();
        }
    }
}