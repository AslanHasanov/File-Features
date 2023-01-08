using DemoApplication.Areas.Admin.ViewComponents;

using DemoApplication.Areas.Admin.ViewModels.UserManagament;
using DemoApplication.Areas.Client.ViewModels.Basket;
using DemoApplication.Database;
using DemoApplication.Database.Models;
using DemoApplication.Migrations;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Text.Json;
using BC = BCrypt.Net.BCrypt;

namespace DemoApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/user")]
    //[Authorize(Roles = "admin")]
    public class UserManagamentController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;




        public UserManagamentController(DataContext dbContext, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        #region List

        [HttpGet("list", Name = "admin-user-list")]
        public ActionResult List()
        {
            var model = _dbContext.Users
                .Select(u => new UserViewModel(u.Id, u.Email, u.FirstName, u.LastName, u.CreatedAt, u.UpdatedAt, u.Role.Name))
                .ToList();

            return View(model);
        }
        #endregion


        #region Add

        [HttpGet("add", Name = "admin-user-add")]
        public async Task<IActionResult> Add()
        {
            var model = new AddViewModel
            {
                RoleListItemViewModels = await _dbContext.Roles.Select(r => new RoleListItemViewModel
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToListAsync(),
            };

            return View(model);
        }

        [HttpPost("add", Name = "admin-user-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await CreateUserAsync();
            var basket = await CreateBasketAsync();


            async Task<Database.Models.User> CreateUserAsync()
            {
                var user = new Database.Models.User
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Password = BC.HashPassword(model.Password),
                    RoleId = model.RoleId
                };
                await _dbContext.Users.AddAsync(user);

                return user;
            }

            //Create basket process
            async Task<Database.Models.Basket> CreateBasketAsync()
            {
                //Create basket process
                var basket = new Database.Models.Basket
                {
                    User = user,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                await _dbContext.Baskets.AddAsync(basket);

                return basket;
            }



            await _dbContext.SaveChangesAsync();


            return RedirectToRoute("admin-user-list");
        }

        #endregion



        [HttpGet("update/{id}", Name = "admin-user-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
            {
                return NotFound();
            }
         

            var model = new UpdateViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
               
                RoleId = user.RoleId,
                RoleListItemViewModels = await _dbContext.Roles.Select(r => new RoleListItemViewModel
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToListAsync(),
                UpdatedAt = DateTime.Now,

            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-user-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromForm] UpdateViewModel model)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }



            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.RoleId = model.RoleId;
            user.UpdatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();
           

          

            return RedirectToRoute("admin-user-list",model);
        }
    }
}