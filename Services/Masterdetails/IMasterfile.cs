using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IntergrationA.Models.barcodemodel;
using static IntergrationA.Models.categorymodel;
using static IntergrationA.Models.inventorymodel;

namespace IntergrationA.Services.Masterdetails
{
    public interface IMasterfile
    {
       Task<string> getsettings(string filtervalue);
       Task<bool> insertBarcodeifnotExists(barcodebaseclass Barcodedata);
       Task<bool> insertCategoriesifnotExists(categorybaseclass categorybaseclass);

       Task<bool> insertInventoryifnotExists(InventoryBaseclass InventoryBaseclass);



    }
}