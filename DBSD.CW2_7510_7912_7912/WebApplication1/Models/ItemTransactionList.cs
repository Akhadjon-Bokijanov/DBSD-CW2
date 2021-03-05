using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBSD_CW2_7510_8775_7912.Models
{
    public class ItemTransactionList
    {
        public int? ItemTransactionListId { get; set; }
        public decimal? Amount { get; set; }
        public ItemTransaction ItemTransaction { get; set; }
        public Item Item { get; set; }
    }
}