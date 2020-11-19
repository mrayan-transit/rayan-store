using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RayanStore.Data;

namespace RayanStore.Areas.Admin.Pages.Orders
{
    [Authorize(Roles = "Admin,Sales")]
    public class IndexModel : PageModel
    {
        private readonly StoreDbContext _dbContext;

        public IndexModel(StoreDbContext dbContext) => _dbContext = dbContext;

        public async Task OnGetAsync()
        {
            Orders = await _dbContext.Orders.AsNoTracking().ToListAsync();
        }

        public List<Order> Orders {get;private set;}
    }
}
