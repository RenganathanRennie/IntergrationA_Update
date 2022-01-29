using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntergrationA.Models
{
    public class categorymodel
    { 
        public class Page
        {
            public int PageSize { get; set; }
            public int PageCount { get; set; }
            public int RecordCount { get; set; }
            public int CurrPage { get; set; }
        }

        public class categorybaseclass
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string Id { get; set; }
            public string Code { get; set; }
            public List<Categories> data { get; set; }
            public Page Page { get; set; }
        }

        public class Categories
        {
            [Key]
            public int seq { get; set; }

            public string cInvCCode { get; set; }

            public string cInvCName { get; set; }

            public string iInvCGrade { get; set; }

            public string bInvCEnd { get; set; }

            public string cEcoCode { get; set; }

            public string cBarCode { get; set; }

            public string pubufts { get; set; }

        }

    }
}