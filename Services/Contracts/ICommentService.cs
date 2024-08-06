using Entities.Dtos;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICommentService
    {
        void CreateComment(CommentDtoForCreate commentDto);
        void UpdateComment(CommentDtoForUpdate commentDto, string userId);
        void DeleteComment(int commentId, string userId);
        IEnumerable<Comment> GetCommentsForProduct(int productId,bool trackChanges);
        Comment GetComment(int commentId, bool trackChanges);
    }
}
