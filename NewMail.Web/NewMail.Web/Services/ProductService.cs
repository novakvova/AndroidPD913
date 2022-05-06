using AutoMapper;
using NewMail.Application.Interfaces;
using NewMail.Data.Entities;
using NewMail.Web.Models;

namespace NewMail.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public int Create(ProductCreateViewModel model)
        {
            var product = _mapper.Map<ProductEntity>(model);
            _productRepository.Add(product);
            return product.Id;
            throw new NotImplementedException();
        }
    }
}
