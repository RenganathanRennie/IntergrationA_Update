using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntergrationA.Models
{
    public class barcodemodel
    {
        public class barcode
    {
        public int seq { get; set; }
        public string ItemCode { get; set; }
        public string Barcode { get; set; }
    }

    public class Page
    {
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int RecordCount { get; set; }
        public int CurrPage { get; set; }
    }

    public class barcodebaseclass
    {
        public bool Success { get; set; }
        public object Message { get; set; }
        public object Id { get; set; }
        public object Code { get; set; }
        public List<barcode> data { get; set; }
        public Page Page { get; set; }
    }
    }
}