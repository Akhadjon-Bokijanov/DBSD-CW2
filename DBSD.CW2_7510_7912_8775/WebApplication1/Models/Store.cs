using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBSD_CW2_7510_8775_7912.Models
{
    public class Store
    {
        public int? StoreId { get; set; }
        public string Name { get; set; }
        public decimal? Square { get; set; }
        public Employee Employee { get; set; }
    }
}