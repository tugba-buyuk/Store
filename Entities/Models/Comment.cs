using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public int Rating { get; set; }
        public string CommentText { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public Product Product { get; set; }
        public IdentityUser User { get; set; }
    }
}
