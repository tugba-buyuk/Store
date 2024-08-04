using Entities.Models;
using Entities.RequestParameters;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly RepositoryContext _context;
        public ProductRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
        public void CreateOneProduct(Product product) => Create(product);

        public void DeleteOneProduct(Product product) =>Remove(product);

        public IQueryable<Product> GetAllProducts(bool trackChanges) => FindAll(trackChanges);
        
        public Product? GetOneProduct(int id,bool trackChanges)
        {
            return FindByCondition(p => p.Id.Equals(id), trackChanges);
        }

        public IQueryable<Product> GetShowCaseProducts(bool trackChanges)
        {
            return FindAll(trackChanges)
            .Where(p => p.ShowCase.Equals(true));
        }

        public IQueryable<Product> GettAllProductsWithDetails(ProductRequestParameters p)
        {
            return _context
                    .Products
                    .FilteredByCategoryId(p.CategoryId)
                    .FilteredBySearchTerm(p.SearchTerm)
                    .FilteredByPrice(p.MinPrice,p.MaxPrice,p.IsValidPrice)
                    .ToPaginate(p.PageNumber,p.PageSize);
                                    
        }

        public IQueryable<Product> ProductsWithSearch(string searchTerm)
        {
           return _context.Products.FilteredBySearchTerm(searchTerm);
        }

        //public void RemoveProductColors(int productId)
        //{
        //    var productColorsToDelete = _context.ProductColors.Where(pc => pc.ProductId == productId).ToList();
        //    _context.ProductColors.RemoveRange(productColorsToDelete);
        //}

        public void UpdateOneProduct(Product entity) => Update(entity);

    }
}
