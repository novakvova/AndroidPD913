using NewMail.Application.Interfaces;
using NewMail.Application.Repositories;
using NewMail.Web.Services;

namespace NewMail.Web.StartUp
{
    public static class InitServices
    {
        public static void UseAppServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
        }
    }
}
