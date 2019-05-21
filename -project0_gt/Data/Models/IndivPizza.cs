using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class IndivPizza
    {
        public int PizzaId { get; set; }
        public int? Ingredient0Fid { get; set; }
        public int? Ingredient1Fid { get; set; }
        public int? Ingredient2Fid { get; set; }
        public int? Ingredient3Fid { get; set; }
        public int? Ingredient4Fid { get; set; }
        public int? CrustFid { get; set; }
        public int? SizeFid { get; set; }
        public int? Count { get; set; }
        public int? OrderFid { get; set; }
        public decimal? Totalcost { get; set; }
    }
}
