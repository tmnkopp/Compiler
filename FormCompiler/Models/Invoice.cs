using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Models
{
    public class Invoice
    { 
        public string Id { get; set; }
        public string Customer { get; set; }
        public string InvoiceNumber { get; set; }
        public string Description { get; set; }
        public List<InvoiceLine> Lines { get; set; }
        public decimal TotalCost { get; set; }
    }
    public class InvoiceLine
    {
        public string ItemName { get; set; }
        public decimal Cost { get; set; }
    }
}
