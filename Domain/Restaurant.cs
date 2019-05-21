using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Restaurant
    {
        public int LocationId { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int? Zipcode { get; set; }
        public string ResName { get; set; }


    }
}
