using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RayanStore.Areas.Admin.Models;
using RayanStore.Data;
using RayanStore.Services;

namespace RayanStore.Areas.Admin.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly StoreDbContext _dbContext;
        private readonly IFileUploader _fileUploader;
        public CreateModel(StoreDbContext dbContext,
            IFileUploader fileUploader)
        {
            _dbContext = dbContext;
            _fileUploader = fileUploader;
        }
            

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            if (ProductModel.ImageFile != null && ProductModel.ImageFile.Length > 0)
                ProductModel.ImageUrl = await _fileUploader.Upload(ProductModel.ImageFile, "products");

            _dbContext.Add(ProductModel.ToEntity());
            await _dbContext.SaveChangesAsync();
            return RedirectToPage("Index");
        }

        [BindProperty]
        public ProductModel ProductModel {get;set;}
    }
}
