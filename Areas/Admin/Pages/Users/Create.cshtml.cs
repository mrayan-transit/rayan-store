using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RayanStore.Areas.Admin.Helpers;
using RayanStore.Areas.Admin.Models;
using RayanStore.Data;

namespace RayanStore.Areas.Admin.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly StoreDbContext _dbContext;

        public CreateModel(StoreDbContext dbContext)
            => _dbContext = dbContext;

        public void OnGet()
        {
            UserModel = new UserModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            //Check email validity
            var emailExists = await _dbContext.Users.AsNoTracking()
                .AnyAsync(x => x.Email == UserModel.Email);
            if (emailExists)
            {
                ModelState.AddModelError(nameof(UserModel.Email), "Email is already used by another user.");
                return Page();
            }

            //Now you're good to go
            var user = new User()
            {
                Email = UserModel.Email,
                FullName = UserModel.FullName,
                Roles = string.Join(',', UserModel.Roles),
                UserName = UserModel.Email,
                PasswordHash = UserModel.Password.GetMd5Hash()
            };

            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();
            return RedirectToPage("Index");
        }

        [BindProperty]
        public UserModel UserModel { get; set;}
    }
}
