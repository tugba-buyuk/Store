using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IProductService
    {
        void CreateProduct(ProductDtoForInsertion productDto);
        void DeleteOneProduct(int id);
        IEnumerable<Product> GetAllProducts(bool trackChanges);
        IEnumerable<Product> GetLastestProducts(int n,bool trackChanges);
        IEnumerable<Product> GetShowCaseProducts(bool trackChanges);
        IEnumerable<Product> GettAllProductsWithDetails(ProductRequestParameters p);
        Product? GetOneProduct(int id, bool trackChanges);
        ProductDtoForUpdate GetOneProductForUpdate(int id, bool trackChanges);
        void UpdateOneProduct(ProductDtoForUpdate productDto);
        IEnumerable<Product> GetProductsWithSearch(string searchTerm);
    }
}
