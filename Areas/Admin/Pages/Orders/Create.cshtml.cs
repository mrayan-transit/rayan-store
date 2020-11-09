using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RayanStore.Areas.Admin.Models;
using RayanStore.Data;
using RayanStore.Services;

namespace RayanStore.Areas.Admin.Pages.Orders
{
    [Authorize(Roles = "Admin,Sales")]
    public class CreateModel : PageModel
    {
        private readonly StoreDbContext _dbContext;

        public CreateModel(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
            

        public async Task OnGetAsync()
        {
            OrderModel = new OrderModel();
            OrderModel.Products = await _dbContext.Products.AsNoTracking()
                .Select(x => new ProductModel() {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                })
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                OrderModel.Products = await _dbContext.Products.AsNoTracking()
                .Select(x => new ProductModel() {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                })
                .ToListAsync();
                return Page();
            }
                

            

            var entity = new Order()
            {
                CustomerEmail = OrderModel.CustomerEmail,
                CustomerName = OrderModel.CustomerName,
                CustomerPhone = OrderModel.CustomerPhone,
                OrderDate = DateTime.Now
            };

            //Select selected products
            var selectedProducts = await _dbContext.Products.AsNoTracking()
                .Where(x => OrderModel.SelectedProductIds.Contains(x.Id))
                .ToListAsync();
            
            //Calculate total amount
            entity.TotalAmount = selectedProducts.Sum(x => x.Price);
            entity.OrderProducts = selectedProducts.Select(x => new OrderProduct() { ProductId = x.Id })
                .ToList();

            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
            return RedirectToPage("Index");
        }

        [BindProperty]
        public OrderModel OrderModel {get;set;}
    }
}
