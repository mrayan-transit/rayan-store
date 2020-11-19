using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
//using RayanStore.Data.Models;

namespace RayanStore.Admin.Pages.Account
{
    [Authorize]
    public class LogoutModel : PageModel
    {
        // private readonly SignInManager<ApplicationUser> _signInManager;

        // public LogoutModel(SignInManager<ApplicationUser> signInManager)
        // {
        //     _signInManager = signInManager;
        // }

        public async Task<IActionResult> OnGet()
        {
            await HttpContext.SignOutAsync();
            return RedirectToPage("Login");
        }
    }
}
