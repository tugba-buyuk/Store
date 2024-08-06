using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CommentManager : ICommentService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public CommentManager(IRepositoryManager manager,IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public void CreateComment(CommentDtoForCreate commentDto)
        {
            Comment comment= _mapper.Map<Comment>(commentDto);
            _manager.Comment.AddComment(comment);
            _manager.Save();
        }

        public void DeleteComment(int commentId, string userId)
        {
            Comment comment = _manager.Comment.GetOneComment(commentId,false);
            if(comment is not null)
            {
                _manager.Comment.DeleteComment(comment,userId);
                _manager.Save();
            }
            else
            {
                throw new Exception("The comment is not found.");
            }
        }

        public IEnumerable<Comment> GetCommentsForProduct(int productId, bool trackChanges)
        {
            return _manager.Comment.GetAllCommentForProduct(productId, trackChanges);
        }

        public void UpdateComment(CommentDtoForUpdate commentDto, string userId)
        {
            Comment comment = _mapper.Map<Comment>(commentDto);
            _manager.Comment.UpdateComment(comment, userId);
            _manager.Save();
            
        }
    }
}
