﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegracaoGoogle.Infra.Data.Context;
using IntegracaoGoogle.Domain.Interfaces;
using IntegracaoGoogle.Domain.Entities;

namespace IntegracaoGoogle.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        ApplicationDbContext _productContext;
        public ProductRepository(ApplicationDbContext context)
        {
            _productContext = context;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _productContext.Add(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetByIdAsync(int? id)
        {
            //return await _productContext.Products.FindAsync(id);
            return await _productContext.Products.Include(c => c.Category).SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductAsync()
        {
            return await _productContext.Products.ToListAsync();
        }

        //public async Task<Product> GetProductCategoryAsync(int? id)
        //{
        //    //eager loading
        //    return await _productContext.Products.Include(c => c.Category).SingleOrDefaultAsync(p => p.Id == id);
        //}

        public async Task<Product> RemoveAsync(Product product)
        {
            _productContext.Remove(product);
            await _productContext.SaveChangesAsync();
            return product;

        }
        public async Task<Product> UpdateAsync(Product product)
        {
                _productContext.Update(product);
                await _productContext.SaveChangesAsync();
                return product;
        }
    }
}
