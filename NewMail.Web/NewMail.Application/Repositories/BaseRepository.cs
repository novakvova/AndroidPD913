using NewMail.Application.Interfaces;
using NewMail.Data;
using NewMail.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMail.Application.Repositories
{
    public class BaseRepository<TEntity, TIdType> : IBaseRepository<TEntity, TIdType> where TEntity : class, IEntity<TIdType>
    {
        private readonly AppEFContext _context;
        public BaseRepository(AppEFContext context)
        {
            _context = context;
        }

        public virtual IQueryable<TEntity> Items => _context.Set<TEntity>().Where(x => !x.IsDeleted);

        public bool Add(TEntity entity)
        {
            try
            {
                _context.Add<TEntity>(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Change(TEntity element)
        {
            try
            {
                _context.Update<TEntity>(element);

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(TIdType id)
        {
            try
            {
                var item = _context.Set<TEntity>().Find(id);
                item.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public TEntity GetById(TIdType id)
        {
            var item = _context.Set<TEntity>().Find(id);
            return item;
        }
    }
}
