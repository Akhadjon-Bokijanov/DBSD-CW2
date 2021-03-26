using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBSD_CW2_7510_8775_7912.Models
{
    public class ItemBind
    {
        public int? ItemBindId { get; set; }
        public Item Parent { get; set; }
        public Item Child { get; set; }
        public decimal ChildAmount { get; set; }
    }
}