using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewMail.Application.Interfaces;
using NewMail.Web.Models;
using NewMail.Web.Services;

namespace NewMail.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productservice;
        public ProductController(IProductService _productservice)
        {
            _productservice = _productservice;
        }
        [HttpGet]   
        public IActionResult GetAll()
        {
            return Ok();
        }
        [HttpGet]
        public IActionResult Add(ProductCreateViewModel model)
        {
            _productservice.Create(model);
            return Ok();
        }
    }
}
