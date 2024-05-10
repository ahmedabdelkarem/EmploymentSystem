using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Employment.Domain.Entities;
using Employment.Domain.IRepository;
using Employment.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;

namespace Employment.Infra.Data.Repository
{
    public class UserRepository : GenericRepository<AspNetUser> , IUserRepository
    {
      //  protected readonly EmploymentContext Db;
        protected readonly DbSet<AspNetUser> DbSet;

        public UserRepository(EmploymentContext DBContext) : base(DBContext)
        {
            DbSet = DBContext.Set<AspNetUser>();
        }
      

        public async Task<AspNetUser> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<AspNetUser>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<AspNetUser> GetByEmail(string email)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
        }

        public void Add(AspNetUser user)
        {
           DbSet.Add(user);
        }

        public void Update(AspNetUser user)
        {
            DbSet.Update(user);
        }

        public void Remove(AspNetUser user)
        {
            DbSet.Remove(user);
        }

        public void Dispose()
        {
            //Db.Dispose();
        }
    }
}
