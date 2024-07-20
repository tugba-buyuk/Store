using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ProductManager(IRepositoryManager manager,IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public void CreateProduct(ProductDtoForInsertion productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            product.Images = productDto.Images;
            _manager.Product.CreateOneProduct(product);
            _manager.Save();
            var lastId = product.Id;
            var savedProduct=_manager.Product.GetOneProduct(lastId,false);
        }

        public void DeleteOneProduct(int id)
        {
            Product product = _manager.Product.GetOneProduct(id, false);
            if(product != null)
            {
                _manager.Product.DeleteOneProduct(product);
                _manager.Save();
            }
        }

        public IEnumerable<Product> GetAllProducts(bool trackChanges)
        {
            return _manager.Product.GetAllProducts(trackChanges).Include(p => p.Category);
        }

        public IEnumerable<Product> GetLastestProducts(int n, bool trackChanges)
        {
            return _manager.Product
                .FindAll(trackChanges)
                .OrderByDescending(prd => prd.Id)
                .Take(n);
        }

        public Product? GetOneProduct(int id, bool trackChanges)
        {
            var product= _manager.Product.GetOneProduct(id, trackChanges);  
            if(product is null)
            {
                throw new Exception("Product not found!");
            }
            return product;
        }

        public ProductDtoForUpdate GetOneProductForUpdate(int id, bool trackChanges)
        {
            var product=GetOneProduct(id,trackChanges);
            var productDto=_mapper.Map<ProductDtoForUpdate>(product);
            productDto.Images = product.Images;
            return productDto;

        }

        public IEnumerable<Product> GetShowCaseProducts(bool trackChanges)
        {
            return _manager.Product.GetShowCaseProducts(trackChanges);
        }

        public IEnumerable<Product> GettAllProductsWithDetails(ProductRequestParameters p)
        {
            return _manager.Product.GettAllProductsWithDetails(p);
        }

        public void UpdateOneProduct(ProductDtoForUpdate productDto)
        {

            var existingProduct = _manager.Product.GetOneProduct(productDto.Id, true);
            if (existingProduct == null)
            {
                throw new Exception("Product not found");
            }
            var updatedProduct = _mapper.Map(productDto, existingProduct);

            _manager.Product.UpdateOneProduct(updatedProduct);
            _manager.Save();
        }
    }
}
