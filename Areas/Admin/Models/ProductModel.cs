using Microsoft.AspNetCore.Http;
using RayanStore.Data;

namespace RayanStore.Areas.Admin.Models
{
    public class ProductModel
    {
        public ProductModel() { }

        public ProductModel(Product entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            ImageUrl = entity.ImageUrl;
            Price = entity.Price;
            IsFeatured = entity.IsFeatured;
        }


        public int Id {get; set;}

        public string Name {get;set;}

        public string Description {get;set;}

        public string ImageUrl {get;set;}

        public IFormFile ImageFile {get;set;}

        public float Price {get;set;}

        public bool IsFeatured {get;set;}

        public Product ToEntity()
        {
            return new Product()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                ImageUrl = ImageUrl,
                IsFeatured = IsFeatured,
                Price = Price
            };
        }
    }
}