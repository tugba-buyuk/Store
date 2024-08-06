using Entities.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ICommentRepository
    {
        void AddComment(Comment comment);
        void UpdateComment(Comment comment, string userId);
        void DeleteComment(Comment comment, string userId);
        Comment? GetOneComment(int id, bool trackChanges);
        IQueryable<Comment> GetAllCommentForProduct(int productId,bool trackChanges);
    }
}
