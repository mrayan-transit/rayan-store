using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RayanStore.Data;

namespace RayanStore.Areas.Admin.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly StoreDbContext _dbContext;

        public IndexModel(StoreDbContext dbContext) => _dbContext = dbContext;

        public async Task OnGetAsync()
        {
            Users = await _dbContext.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public List<User> Users {get;set;}
    }
}
