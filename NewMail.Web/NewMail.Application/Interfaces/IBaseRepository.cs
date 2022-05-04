using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMail.Application.Interfaces
{
    public interface IBaseRepository<TEntity, TIdType>
    {
        public IQueryable<TEntity> Items { get; }
        public bool Delete(TIdType id);
        public bool Add(TEntity entity);
        public bool Change(TEntity entity);
        public TEntity GetById(TIdType id);

    }
}
