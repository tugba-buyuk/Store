using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public record CommentDto
    {
        public int CommentId { get; init; }
        public int ProductId { get; init; }
        public string UserId { get; init; }
        public int Rating { get; init; }
        public string CommentText { get; init; } = string.Empty;
        public DateTime CreatedDate { get; init; }

    }
}
