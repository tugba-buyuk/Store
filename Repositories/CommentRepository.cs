using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RepositoryContext _context;
        public CommentRepository(RepositoryContext context, UserManager<IdentityUser> userManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
        }

        public void AddComment(Comment comment)=>Create(comment);


        public void DeleteComment(Comment comment, string userId)
        {
            if(comment.UserId == userId)
            {
                Remove(comment);
            }
            else
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this review.");
            }
        }

        public IQueryable<Comment> GetAllCommentForProduct(int productId, bool trackChanges)
        {
            return FindAll(trackChanges).Where(c => c.ProductId == productId).Include(r => r.User);
        }

        public Comment? GetOneComment(int id, bool trackChanges)
        {
            return FindByCondition(p => p.Id.Equals(id), trackChanges);

        }

        public void UpdateComment(Comment comment, string userId)
        {
            if(comment.UserId == userId)
            {
                Update(comment);
            }
            else
            {
                throw new UnauthorizedAccessException("You are not authorized to update this review.");
            }
        }

    }
}
