using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewMail.Data;
using NewMail.Data.Entities.Identity;
using NewMail.Web.Helpers;
using NewMail.Web.Models;
using NewMail.Web.Services;
using System.Drawing.Imaging;

namespace NewMail.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<AppUser> _userManager;
        //private readonly ILogger<AccountController> _logger;
        private readonly AppEFContext _context;
        public AccountController(UserManager<AppUser> userManager,
            IJwtTokenService jwtTokenService, IMapper mapper, AppEFContext context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
            _context = context;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var img = ImageWorker.FromBase64StringToImage(model.Photo);
            string randomFilename = Path.GetRandomFileName() + ".jpeg";
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "uploads", randomFilename);
            img.Save(dir, ImageFormat.Jpeg);
            var user = _mapper.Map<AppUser>(model);
            user.Photo = randomFilename;
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors });


            return Ok(new { token = _jwtTokenService.CreateToken(user) });
        }

        [HttpGet]
        [Authorize]
        [Route("users")]
        public async Task<IActionResult> Users()
        {
            //throw new AppException("Дика собака!");
            var list = _context.Users.Select(x => _mapper.Map<UserItemViewModel>(x)).ToList();

            return Ok(list);
        }

        [HttpGet]
        //[Authorize]
        [Route("getuser/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //throw new AppException("Email or password is incorrect");
            Thread.Sleep(1000);
            var user = _context.Users
                .FirstOrDefault(x => x.Id == id);
            if (user == null)
                return NotFound();
            return Ok(_mapper.Map<UserItemViewModel>(user));
        }

        /// <summary>
        /// Авторизація на API
        /// </summary>
        /// <param name="model">пошта та пароль користувача</param>
        /// <returns>JWT - token sha256</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Login user</response>
        /// <response code="400">Login has missing/invalid values</response>
        /// <response code="500">Oops! Can't create your login right now</response>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        return Ok(new { token = _jwtTokenService.CreateToken(user) });
                    }
                }
                return BadRequest(new { error = "Користувача не знайдено" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Помилка на сервері" });
            }
        }
    
    
    }
}
