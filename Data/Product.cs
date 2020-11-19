using System.Collections.Generic;

namespace RayanStore.Data
{
    public class Product
    {
        public int Id {get; set;}

        public string Name {get;set;}

        public string Description {get;set;}

        public string ImageUrl {get;set;}

        public float Price {get;set;}

        public bool IsFeatured {get;set;}

        public ICollection<OrderProduct> OrderProducts {get;set;}
    }
}