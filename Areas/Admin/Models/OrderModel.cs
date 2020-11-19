using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RayanStore.Data;

namespace RayanStore.Areas.Admin.Models
{
    public class OrderModel
    {
        public int Id {get;set;}

        [Required]
        [Display(Name = "Customer Name")]
        public string CustomerName {get;set;}

        [Required]
        [Display(Name = "Customer Phone")]
        public string CustomerPhone {get;set;}

        [Display(Name = "Customer Email")]
        [DataType(DataType.EmailAddress)]
        public string CustomerEmail {get;set;}

        [Display(Name = "Total Amount")]
        public float TotalAmount {get;set;}

        [Display(Name = "Order Date")]
        public DateTime OrderDate {get;set;}

        [Display(Name = "Select Products")]
        public int[] SelectedProductIds {get;set;}

        public List<ProductModel> Products {get;set;}

    }
}