using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities.Dtos
{
    public record ProductDto
    {
        public int Id { get; init; }
        [Required(ErrorMessage = "ProductName is required")]
        public string? ProductName { get; init; } = string.Empty;
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; init; }
        public int? CategoryId { get; init; }      // Foreign key
        public string? Summary { get; init; } = string.Empty;
        public bool ShowCase { get; set; }
        public List<string> Sizes { get; set; } = new List<string>();
        public List<string> ColorNames { get; set; } = new List<string>();
        public string? Gender { get; set; } = string.Empty;
        public List<PrdImage> Images { get; set; } = new List<PrdImage>();
        public List<IFormFile> Files { get; set; } = new List<IFormFile>(); // Birden fazla resim dosyası için
        public List<string> ImageUrls { get; set; } = new List<string>();

    }
}
