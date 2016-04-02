﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheatPads.Api.Data.Models
{
    public class Product
    {
        public string Sku { get; set; }
        public string Title { get; set; } 
        public string Details { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public virtual Category Category { get; set; }
    }
}
