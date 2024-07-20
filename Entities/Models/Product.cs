using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace Entities.Models;

public class Product
{
    public int Id { get; set; }
    public string? ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int? CategoryId { get; set; }      //Foreign key
    public Category? Category { get; set; }   //Navigation property
    public String? Summary { get; set; }=String.Empty;
    public bool ShowCase {  get; set; }
    public List<string> Sizes { get; set; } = new List<string>();
    public List<string> ColorNames { get; set; } = new List<string>();
    public string? Gender { get; set; } = string.Empty;
    public List<PrdImage> Images { get; set; } = new List<PrdImage>(); // Resim koleksiyonu
    public List<string> ImageUrls { get; set; } = new List<string>();
}
    


