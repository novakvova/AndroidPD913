using NewMail.Web.Models;

namespace NewMail.Web.Services
{
    public interface IProductService
    {
        public int Create(ProductCreateViewModel model);
    }
}
