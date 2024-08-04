using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Extensions
{
    public static class ProductRepositoryExtension
    {
        public static IQueryable<Product> FilteredByCategoryId(this IQueryable<Product> products,int? categoryId)
        {
            if(categoryId is null)
            {
                return products;
            }
            else
            {
                return products.Where(prd=>prd.CategoryId.Equals(categoryId));
            }
        }

        public static IQueryable<Product> FilteredBySearchTerm(this IQueryable<Product> products, string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return products;
            }

            var keywords = searchTerm.Trim().ToLower().Split(' ');

            return products.Where(prd =>
                keywords.Any(keyword =>
                    prd.ProductName.ToLower().Contains(keyword) ||
                    prd.Summary.ToLower().Contains(keyword) ||
                    prd.Gender.ToLower().Contains(keyword) ||
                    prd.ColorNames.Any(color => color.ToLower().Contains(keyword))
                )
            );
        }
        public static IQueryable<Product>FilteredByPrice(this IQueryable<Product> products,
            int minPrice, int maxPrice, bool isValidPrice)
        {
            if (isValidPrice)
                return products.Where(prd => prd.Price >= minPrice && prd.Price <= maxPrice);
            else
                return products;
        }
        public static IQueryable<Product> ToPaginate(this IQueryable<Product> products,int pageNumber, int pageSize)
        {
            return products
                .Skip((pageNumber-1)*pageSize)
                .Take(pageSize);
        } 


    }
}
