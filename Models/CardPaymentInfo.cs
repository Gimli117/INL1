using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace MenuTemplateForINL1.Models
{
    internal class CardPaymentInfo
    {
        [Key]
        public int? Id { get; set; }
        public string? CardName { get; set; }
        public string? CardNumber { get; set; }
        public string? ExpiryDate { get; set; }
        public string? CVC { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}