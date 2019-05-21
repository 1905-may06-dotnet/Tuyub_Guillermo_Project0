using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Ingredients
    {
        public int IngId { get; set; }
        public string Ingredient { get; set; }
        public decimal? Cost { get; set; }
    }
}
