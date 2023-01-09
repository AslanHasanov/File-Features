using DemoApplication.Areas.Client.ActionFilter;
using DemoApplication.Areas.Client.ViewModels.Authentication;
using DemoApplication.Contracts.Email;
using DemoApplication.Contracts.Identity;
using DemoApplication.Database;
using DemoApplication.Database.Models;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace DemoApplication.Controllers
{
    [Area("client")]
    [Route("auth")]
    public class AuthenticationController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;


        public AuthenticationController(DataContext dbContext, IUserService userService, IEmailService emailService)
        {
            _dbContext = dbContext;
            _userService = userService;
            _emailService = emailService;
        }

        #region Login and Logout

        [HttpGet("login", Name = "client-auth-login")]
        [ServiceFilter(typeof(ValidationCurrentUserAttribute))]
        public async Task<IActionResult> LoginAsync()
        {
            if (_userService.IsAuthenticated)
            {
                return RedirectToRoute("client-account-dashboard");
            }

            return View(new LoginViewModel());
        }

        [HttpPost("login", Name = "client-auth-login")]
        public async Task<IActionResult> LoginAsync(LoginViewModel? model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!await _userService.CheckPasswordAsync(model!.Email, model!.Password))
            {
                ModelState.AddModelError(String.Empty, "Email or password is not correct");
                return View(model);
            }
            if (await _dbContext.Users.AnyAsync(u => u.Email == model.Email && u.Role.Name == RoleNames.ADMIN))
            {
                await _userService.SignInAsync(model!.Email, model!.Password, RoleNames.ADMIN);
                return RedirectToRoute("admin-auth-login");
            }

            await _userService.SignInAsync(model!.Email, model!.Password);

            return RedirectToRoute("client-home-index");
        }

        [HttpGet("logout", Name = "client-auth-logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await _userService.SignOutAsync();

            return RedirectToRoute("client-home-index");
        }

        #endregion

        #region Register

        [HttpGet("register", Name = "client-auth-register")]
        [ServiceFilter(typeof(ValidationCurrentUserAttribute))]
        public ViewResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost("register", Name = "client-auth-register")]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var emails = new List<string>();

            emails.Add(model.Email);


            await _userService.CreateAsync(model);
            var message = new MessageDto(emails, "Activate Email", $"https://localhost:7026/authentication/email?email={model.Email}");

            _emailService.Send(message);


            return RedirectToRoute("client-home-index");
        }

        #endregion
        [HttpGet("email", Name = "client-auth-email")]
        public async Task<IActionResult> Email(string email)
        {
            var userEmail = await _dbContext.Users.FirstOrDefaultAsync(e => e.Email == email);

            if (userEmail is null)
            {
                return NotFound();
            }

            var time = DateTime.Now.Minute - userEmail.CreatedAt.Minute;

            if (time <= 15)
            {
                userEmail.IsActive = true;
            }
            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("client-home-index");

        }
    }
}
