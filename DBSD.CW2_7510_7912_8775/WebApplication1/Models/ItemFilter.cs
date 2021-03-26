using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBSD_CW2_7510_8775_7912.Models
{
    public class ItemFilter
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public int? SortIndex { get; set; }
        public string SortCase { get; set; }
        public string StoreName { get; set; }
        public string LocalName { get; set; }
        public string GlobalName { get; set; }
        public int? ItemUID { get; set; }
        public string SupplierName { get; set; }
        public string MadeOf { get; set; }
    }
}