using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public record CategoryDto
    {
        public int Id { get; init; }
        [Required(ErrorMessage = "CategoryName is required")]
        public string? CategoryName { get; init; } = string.Empty;
    }
}
