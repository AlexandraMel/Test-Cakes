using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CakeExchange.Models
{
   
    public abstract class Product
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Количество:")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Некорректное количество")]
        public int Count { get; set; }

        [Display(Name = "Цена:")]
        [Range(1, Double.MaxValue, ErrorMessage = "Некорректная цена")]
        public decimal Price { get; set; }

        [Display(Name = "Дата:")]
        public DateTime Date { get; set; }

        [Display(Name = "email:")]
        [EmailAddress(ErrorMessage = "Некорректный email")]
        public string Email { get; set; }

    }

    public class Offer : Product
    {

    }
    public class Purchase : Product
    {

    }
}
