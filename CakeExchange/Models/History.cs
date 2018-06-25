using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CakeExchange.Models
{
    public class History
    {
        [Required]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime DatePurchase { get; set; }
        public DateTime DateOffer { get; set; }

        public string EmailPurchase { get; set; }
        public string EmailOffer { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }
    }
}
