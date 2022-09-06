using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext db;

        public ProductRepository(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<bool> Create(Product entity)
        {
            await db.Products.AddAsync(entity);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Product entity)
        {
            db.Products.Remove(entity);
            await db.SaveChangesAsync();

            return true;
        }
       
        public IQueryable<Product> GetAll()
        {
            return db.Products;
        }

        public IQueryable<ProductType> GetTypes()
        {
            return db.ProductTypes;
        }

        public async Task<Product> Update(Product entity)
        {
            db.Products.Update(entity);
            await db.SaveChangesAsync();

            return entity;
        }
    }
}
