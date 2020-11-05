using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RayanStore.Data;

namespace RayanStore.Areas.Admin.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly StoreDbContext _dbContext;
        public CreateModel(StoreDbContext dbContext)
            => _dbContext = dbContext;

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _dbContext.Add(Product);
            await _dbContext.SaveChangesAsync();
            return RedirectToPage("Index");
        }

        [BindProperty]
        public Product Product {get;set;}
    }
}
