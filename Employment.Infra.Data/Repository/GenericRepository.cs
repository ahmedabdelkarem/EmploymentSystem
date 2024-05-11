using Employment.Domain.Entities;
using Employment.Domain.IRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Infra.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly EmploymentContext _dbContext;

        public GenericRepository(EmploymentContext DBContext)
        {
            _dbContext = DBContext;


        }
        public void Dispose()
        {
           
        }

        public IEnumerable<T> GetAll(DbContext _context)
        {
            throw new NotImplementedException();
        }

        public T GetById(DbContext _context, int id)
        {
            return _context.Find<T>(id);

        }

        public bool Insert(DbContext _context, T _obj)
        {
            _context.Add<T>(_obj);
            return SaveChanges(_context);
        }

        public bool Insert(DbContext _context, ref T _obj)
        {
            _context.Add<T>(_obj);
            return SaveChanges(_context);
        }

        public bool Update(DbContext _context, T _obj)
        {
           _context.Update<T>(_obj);
           
            return SaveChanges(_context);
        }

        public bool Delete(DbContext _context, int Id)
        {
            T results = GetById(_context, Id);
            _context.Remove(results);
            return SaveChanges(_context);
           
        }
        public bool SaveChanges(DbContext _context)
        {
            int result = _context.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }


    }
}
