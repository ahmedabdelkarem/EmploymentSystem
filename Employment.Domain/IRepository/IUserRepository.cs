using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Employment.Domain.Entities;
using Employment.Infra.Data;
using NetDevPack.Data;

namespace Employment.Domain.IRepository
{
    public interface IUserRepository 
    {
        Task<AspNetUser> GetById(Guid id);
        Task<AspNetUser> GetByEmail(string email);
        Task<IEnumerable<AspNetUser>> GetAll();

        void Add(AspNetUser user);
        void Update(AspNetUser user);
        void Remove(AspNetUser user);
    }
}