using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RayanStore.Areas.Admin.Helpers;
using RayanStore.Data;

namespace RayanStore.Admin.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly StoreDbContext _dbContext;
        private readonly IWebHostEnvironment _environment;

        public LoginModel(IWebHostEnvironment environment,
            StoreDbContext dbContext)
        {
            _environment = environment;
            _dbContext = dbContext;
        }

        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
                return Page();
            else
                return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            
            var user = await _dbContext.Users.AsNoTracking()
                    .Where(x => x.UserName == Email)
                    .FirstOrDefaultAsync();
                    
            //Check username and password
            if (user.PasswordHash != Password.GetMd5Hash())
            {
                //Invalid login attempt
                ModelState.AddModelError("", "Email or password is invalid.");
                return Page();    
            }

            //User's credentials are valid
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("FullName", user.FullName)
            };
            
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties()
            {
                IsPersistent = RememberMe
            };

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                claimsPrincipal, 
                authProperties);
            return RedirectToPage("/Index");
        }

        #region Properties

        public bool DisplayNotConfirmedError {get;private set;}

        [BindProperty] 
        [Required]
        [DataType(DataType.EmailAddress)] 
        [Display(Prompt="Email")]
        public string Email {get;set;}

        [BindProperty] 
        [Required]
        [DataType(DataType.Password)]
        [Display(Prompt="Password")]
        public string Password {get;set;}

        [BindProperty]
        public bool RememberMe {get;set;}

        #endregion
    }
}
