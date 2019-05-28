using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Inventory
    {
        public int Invid { get; set; }
        public int? Resfid { get; set; }
        public int? FkIngredient { get; set; }
        public decimal? Cost { get; set; }
        public int? Stock { get; set; }
    }
}
