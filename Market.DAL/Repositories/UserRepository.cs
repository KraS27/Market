using Market.DAL.Interfaces;
using Market.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.DAL.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {
        private readonly ApplicationContext db;

        public UserRepository(ApplicationContext applicationContext)
        {
            db = applicationContext;
        }

        public async Task<bool> Create(User entity)
        {
            await db.Users.AddAsync(entity);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(User entity)
        {
            db.Remove(entity);
            await db.SaveChangesAsync();

            return true;
        }

        public IQueryable<User> GetAll()
        {
            return db.Users;
        }

        public async Task<User> Update(User entity)
        {
            db.Update(entity);
            await db.SaveChangesAsync();

            return entity;
        }
    }
}
