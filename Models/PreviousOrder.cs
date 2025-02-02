using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuTemplateForINL1.Models
{
    internal class PreviousOrder
    {
        [Key]
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? OrderNum { get; set; }
        public Customer? Customer { get; set; }
    }
}