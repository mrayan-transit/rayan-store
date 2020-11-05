using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RayanStore.Data;

namespace RayanStore.Areas.Admin.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly StoreDbContext _dbContext;

        public EditModel(StoreDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<IActionResult> OnGetAsync()
        {
            Product = await _dbContext.Products
                .AsNoTracking()
                .Where(x => x.Id == Id)
                .SingleOrDefaultAsync();

            if (Product == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var entity = await _dbContext.Products.FindAsync(Id);
            if (entity == null)
                return NotFound();

            

            //Bind properties
            entity.Name = Product.Name;
            entity.Description = Product.Description;
            entity.IsFeatured = Product.IsFeatured;
            entity.Price = Product.Price;

            await _dbContext.SaveChangesAsync();
            return RedirectToPage("Index");
        }

        [BindProperty(SupportsGet = true)]
        public int Id {get;set;}

        [BindProperty]
        public Product Product {get;set;}
    }
}
