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
    public class IndexModel : PageModel
    {
        private readonly StoreDbContext _dbContext;

        public IndexModel(StoreDbContext dbContext) => _dbContext = dbContext;

        public async Task OnGetAsync()
        {
            Products = await _dbContext.Products.AsNoTracking().ToListAsync();
        }

        public List<Product> Products {get;private set;}
    }
}
