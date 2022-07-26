using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IntergrationA.Models.barcodemodel;
using static IntergrationA.Models.categorymodel;
using static IntergrationA.Models.inventorymodel;
using static IntergrationA_Update.Models.domodel;
using static IntergrationA_Update.Models.somodel;

namespace IntergrationA_Update.Services.Masterdetails
{
    public interface IGetmasterdata
    {
          Task<InventoryBaseclassreturn> GetInventoryBaseclass();
          Task<barcodebaseclass> GetBarcodeBaseclass();
          Task<categorybaseclass> GetCategoryBaseclass();

        Task<string> getOrder(DateTime filter, DateTime filter2);
        Task<string> getOrder(string filter);
        Task<string> getdo(DateTime filter,DateTime filter2);
        Task<string> getdo(string filter);       

          Task<bool> postdo(deliveryOrder domodelsummary)  ; 
          
    }
}