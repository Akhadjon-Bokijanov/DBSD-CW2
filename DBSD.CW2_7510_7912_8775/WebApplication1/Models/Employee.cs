using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBSD_CW2_7510_8775_7912.Models
{
    public class Employee
    {
        public int? EmployeeId { get; set; }
        public string FullName { get; set; }
        public DateTime? DoB { get; set; }
        public decimal? Salary { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}