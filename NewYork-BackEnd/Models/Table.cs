using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewYork_BackEnd.Models
{
    public class Table
    {
        public int TableID { get; set; }
        public string TableName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public int ManagerID { get; set; }
        public User Manager { get; set; }
    }
}
