using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBSD_CW2_7510_8775_7912.Models
{
    public class ItemTransaction
    {
        public int? ItemTransactionId { get; set; }
        public string Notes { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? Operation { get; set; }
        public Employee Employee { get; set; }
    }
}