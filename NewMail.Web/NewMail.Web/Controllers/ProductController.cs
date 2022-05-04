using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewMail.Application.Interfaces;

namespace NewMail.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]   
        public IActionResult GetAll()
        {
            return Ok(_productRepository.Items.ToList());
        }
    }
}
