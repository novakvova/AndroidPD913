using NewMail.Data;
using NewMail.Web.Helpers;
using NewMail.Web.Middleware;
using NewMail.Web.StartUp;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.UseAppDbConext();

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
});

builder.UseAppServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.UseAppAuthJWT();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.UseAppSwaggerGen();

var app = builder.Build();

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UserLoggerFile();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{assemblyName} v1"));
//}

app.UseAppStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.SeedData();

app.MapControllers();

app.Run();
