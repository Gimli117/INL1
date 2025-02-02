using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuTemplateForINL1.Models
{
    internal class Customer
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? Postal { get; set; }
        public string? Street { get; set; }
        public List<PreviousOrder>? PreviousOrders { get; set; }
    }
}