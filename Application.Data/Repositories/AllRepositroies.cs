using Application.Data.ModelContexts;
using Application.Data.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories
{
    public class AllRepositroies<T> : IAllRepositories<T> where T : class
    {
        GiayDBContext _context;
        DbSet<T> _dbset;
        public AllRepositroies()
        {
            
        }
        public AllRepositroies(GiayDBContext context,DbSet<T> dbset)
        {
            _context = context;
            _dbset = dbset;
        }
        public bool CreateItem(T item)
        {
            try
            {
                _dbset.Add(item);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteItem(T item)
        {
            try
            {
                _dbset.Remove(item);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IEnumerable<T> GetAllItems()
        {
            return _dbset.ToList();
        }

        public bool UpdateItem(T item)
        {
            try
            {
                _dbset.Update(item);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
