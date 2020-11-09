using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RayanStore.Areas.Admin.Models;
using RayanStore.Data;

namespace RayanStore.Areas.Admin.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly StoreDbContext _dbContext;

        public EditModel(StoreDbContext dbContext) => 
            _dbContext = dbContext;

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _dbContext.Users.AsNoTracking()
                .Where(x => x.Id == Id)
                .SingleOrDefaultAsync();
            
            if (user == null)
                return NotFound();

            UserModel = new UserModel(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var entity = await _dbContext.Users.FindAsync(Id);
            if (entity == null)
                return NotFound();

            //Check if for email validity
            var emailExists = await _dbContext.Users.AsNoTracking()
                .AnyAsync(x => x.Email == UserModel.Email && x.Id != UserModel.Id);
            if (emailExists)
            {
                ModelState.AddModelError(nameof(UserModel.Email), "Email is already used by another user.");
                return Page();
            }

            //Now you're good to go
            entity.Email = UserModel.Email;
            entity.FullName = UserModel.FullName;
            entity.Roles = string.Join(',', UserModel.Roles);

            await _dbContext.SaveChangesAsync();
            return RedirectToPage("Index");
        }

        [BindProperty(SupportsGet = true)]
        public int Id {get;set;}

        [BindProperty]
        public UserModel UserModel {get;set;}
    }
}
