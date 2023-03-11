using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.DataBaseContext;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class ProductRepository
    {
        private readonly FundooContext fundooContext;
       // private readonly IConfiguration configuration
       public ProductRepository(FundooContext fundooContext)
       {
            this.fundooContext = fundooContext;
       }

        public ProductEntity AddProduct(ProductModel productModel)
        {
            try
            {
                ProductEntity entity = new ProductEntity();
                entity.ProductName = productModel.ProductName;
                entity.Quantity = productModel.Quantity;
                entity.Price = productModel.Price;
                fundooContext.Add(entity);
                fundooContext.SaveChanges();
                return entity;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductEntity> GetAllProducts(long productId)
        {
            var productList = fundooContext.ProductTable.Where(a => a.ProductId == productId).ToList();
            if(productList != null)
            {
                return productList;
            }
            else
            {
                return null;
            }
        }
    }
}
