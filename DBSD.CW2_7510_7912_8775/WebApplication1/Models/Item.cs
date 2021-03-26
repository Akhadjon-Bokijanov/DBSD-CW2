using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBSD_CW2_7510_8775_7912.DAL;
using System.Data.SqlClient;
using System.Data;

namespace DBSD_CW2_7510_8775_7912.Models
{
    public class Item
    {

        public int? ItemId { get; set; }
        public string LocalName { get; set; }
        public string GlobalName { get; set; }
        public int ItemUID { get; set; }
        public DateTime? UsageStartedAt { get; set; }
        public bool IsEchangeble { get; set; }
        public byte[] Image { get; set; }
        public string MadeOf { get; set; }
        public int? TransactionCount { get; set; }
        public int? NumParent { get; set; }
        public decimal? AvgAmountUsagePerParent { get; set; }

        public Unit Unit { get; set; }
        public Store Store { get; set; }
        public Supplier Supplier { get; set; }

        public List<Item> Parents { get; set; }
        public List<Item> Childeren { get; set; }
        public decimal ChildAmount { get; set; }
    }
}