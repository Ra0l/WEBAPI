using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBFACTURA.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public DateTime Date { get; set; }
        public string InvoiceNumber { get; set; }
        //--1 Invoice have many details
        public List<Detail> Details { get; set; }
    }
}