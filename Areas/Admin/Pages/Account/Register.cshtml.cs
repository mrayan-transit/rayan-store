using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RayanStore.Areas.Admin.Helpers;
using RayanStore.Data;
// using RayanStore.Admin.Services.Email;
// using RayanStore.Data.Models;

namespace RayanStore.Admin.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly StoreDbContext _dbContext;

        public RegisterModel(StoreDbContext dbContext)
            => _dbContext = dbContext;
            
        #region Handlers

        public async Task<IActionResult> OnGetAsync()
        {
            //There shouldn't be admins to register
            bool dbHasUsers = (await _dbContext.Users.AsNoTracking().AnyAsync());
            if (dbHasUsers)
                return RedirectToPage("Login");
            
            //Otherwise, user can register.
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            if (Password != ConfirmPassword)
            {
                ModelState.AddModelError(nameof(ConfirmPassword), "Retyping password must match password.");
                return Page();
            }

            //Check if username already exists.
            bool userNameExists = await _dbContext.Users.AsNoTracking()
                .AnyAsync(x => x.UserName == Email);

            if (userNameExists)
            {
                ModelState.AddModelError("", "Email is already used.");
                return Page();
            }
            
            var user = new User()
            {
                FullName = FullName,
                Email = Email,
                UserName = Email,
                PasswordHash = Password.GetMd5Hash()
            };
            
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return RedirectToPage("RegisterConfirmation");
            
        }

        // private async Task<bool> sendVerificationEmail(ApplicationUser user)
        // {
        //     string emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        //     string emailConfirmationLink = Url.Page("VerifyAccount", 
        //         null,
        //         new { userId = user.Id, code = emailConfirmationToken},
        //         Request.Scheme);

        //     string subject = "Verify Your Account";

        //     StringBuilder emailMessage = new StringBuilder();
        //     emailMessage.AppendFormat("<p>Dear {0}, </p><br>", user.FirstName);
        //     emailMessage.AppendFormat("<p>Please confirm your registration as Administrator to My Blog Control Panel by clicking on the link below:</p>");
        //     emailMessage.AppendFormat("<div style=\"border:1px solid #b2b2b2;background-color:#f2f2f2;padding:5px\"><a href=\"{0}\">{0}</a></div>",
        //         emailConfirmationLink);

        //     return await _emailSender.SendAsync(user.Email, subject, emailMessage.ToString());
        // }

        #endregion

        #region Properties

        [Required]
        [Display(Prompt = "Full name")]
        [BindProperty]
        public string FullName {get;set;}

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Prompt="Email")]
        [BindProperty]
        public string Email {get;set;}

        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Password", Prompt="Password")]
        [BindProperty]
        public string Password {get;set;}

        [Required]
        [DataType(DataType.Password)]
        [Display(Prompt="Retype password")]
        [BindProperty]
        public string ConfirmPassword {get;set;}

        #endregion

    }
}