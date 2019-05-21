using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class ResLocation
    {
        public int LocationId { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int? Zipcode { get; set; }
        public string ResName { get; set; }
    }
}
