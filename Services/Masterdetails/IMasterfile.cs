using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IntergrationA.Models.barcodemodel;

namespace IntergrationA.Services.Masterdetails
{
    public interface IMasterfile
    {
       Task<string> getsettings(string filtervalue);
       Task<bool> insertBarcodeifnotExists(barcodebaseclass Barcodedata);

    }
}