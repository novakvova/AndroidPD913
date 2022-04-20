using NewMail.Web.Services;

namespace NewMail.Web.StartUp
{
    public static class InitServices
    {
        public static void UseAppServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
        }
    }
}
