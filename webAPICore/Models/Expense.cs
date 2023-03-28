using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webAPICore.Models
{
    public class Expense
    {
         public DateTime Date { get; set; }
         public decimal Amount { get; set; }
         public string Subject { get; set; }
    }
}