using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_153504_Bagrovets.Domain.Entities
{
    public class CartItem
    {
        public Product Item { get; set; }
        public int Count { get; set; }
    }
}
