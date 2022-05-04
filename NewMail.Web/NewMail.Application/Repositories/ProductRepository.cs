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
    public class ProductRepository : BaseRepository<ProductEntity, int>, IProductRepository
    {
        public ProductRepository(AppEFContext context) : base(context)
        {
        }
    }
}
