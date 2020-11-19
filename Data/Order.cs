using System;
using System.Collections.Generic;

namespace RayanStore.Data
{
    public class Order
    {
        public int Id {get;set;}

        public string CustomerName {get;set;}

        public string CustomerPhone {get;set;}

        public string CustomerEmail {get;set;}

        public float TotalAmount {get;set;}

        public DateTime OrderDate {get;set;}

        public ICollection<OrderProduct> OrderProducts {get;set;}
    }
}