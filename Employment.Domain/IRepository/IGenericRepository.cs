using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Domain.IRepository
{
    public interface  IGenericRepository<T> : IDisposable
    {
        IEnumerable<T> GetAll(DbContext _context);
        T GetById(DbContext _context, int id);
        bool Insert(DbContext _context, T _obj);
        bool Insert(DbContext _context, ref T _obj);
        bool Update(DbContext _context, T _obj);
        bool Delete(DbContext _context, int id);
        bool SaveChanges(DbContext _context);

    }
}
