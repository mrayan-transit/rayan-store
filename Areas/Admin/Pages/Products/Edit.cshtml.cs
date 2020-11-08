using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RayanStore.Areas.Admin.Models;
using RayanStore.Data;
using RayanStore.Services;

namespace RayanStore.Areas.Admin.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly StoreDbContext _dbContext;
        private readonly IFileUploader _fileUploader;

        public EditModel(StoreDbContext dbContext, IFileUploader fileUploader)
        {
            _dbContext = dbContext;
            _fileUploader = fileUploader;
        }
            
        public async Task<IActionResult> OnGetAsync()
        {
            ProductModel = await _dbContext.Products
                .AsNoTracking()
                .Where(x => x.Id == Id)
                .Select(x => new ProductModel(x))
                .SingleOrDefaultAsync();

            if (ProductModel == null)
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

            if (ProductModel.ImageFile != null && 
                ProductModel.ImageFile.Length > 0)
            {
                _fileUploader.Delete(entity.ImageUrl);
                ProductModel.ImageUrl = await _fileUploader.Upload(ProductModel.ImageFile, "products");
            }

            //Bind properties
            entity.Name = ProductModel.Name;
            entity.Description = ProductModel.Description;
            entity.IsFeatured = ProductModel.IsFeatured;
            entity.Price = ProductModel.Price;
            entity.ImageUrl = ProductModel.ImageUrl;

            await _dbContext.SaveChangesAsync();
            return RedirectToPage("Index");
        }

        [BindProperty(SupportsGet = true)]
        public int Id {get;set;}

        [BindProperty]
        public ProductModel ProductModel {get;set;}
    }
}
