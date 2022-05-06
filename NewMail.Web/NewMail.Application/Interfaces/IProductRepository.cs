using NewMail.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMail.Application.Interfaces
{
    public interface IProductRepository : IBaseRepository<ProductEntity, int>
    {

    }
}
