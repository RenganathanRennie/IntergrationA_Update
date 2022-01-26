using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntergrationA.Models
{
    public class barcodemodel
    {
        public class barcodejson
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
        public string Message { get; set; }
        public string Id { get; set; }
        public string Code { get; set; }
        public List<barcodejson> data { get; set; }
        public Page Page { get; set; }
    }
    public class Barcode
{
    [Key]
    public int seq { get; set; }

    public string cNo { get; set; }

    public string cMaker { get; set; }

    public string U8CUSTDEF_0001_E001_F003 { get; set; }

    public string U8CUSTDEF_0001_E001_F005 { get; set; }

    public string U8CUSTDEF_0001_E001_F004 { get; set; }

    public string U8CUSTDEF_0001_E001_PK { get; set; }

    public string iswfcontrolled { get; set; }

    public string iverifystate { get; set; }

    public string ireturncount { get; set; }

    public int? UAPRuntime_RowNO { get; set; }

}
    }
}