using System;
using System.Globalization;
using System.Text.Json;
using System.Threading.Tasks;
using IntergrationA_Update.Services.Masterdetails;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi;
using static IntergrationA.Models.inventorymodel;
using static IntergrationA_Update.Models.domodel;

namespace IntergrationA_Update.Controllers
{
    public class MasterData : ControllerBase
    {
        DataBaseContext context;
        private IGetmasterdata objgetmasterdata;
        public MasterData(DataBaseContext context, IGetmasterdata objgetmasterdata)
        {
            this.objgetmasterdata = objgetmasterdata;
            this.context = context;
        }

        [HttpGet]
        [Authorize]
        [Route("GetInventory")]
        public async Task<IActionResult> get()
        {
            var result = await objgetmasterdata.GetInventoryBaseclass();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetBarcode")]
        public async Task<IActionResult> GetBarcode()
        {
            var result = await objgetmasterdata.GetBarcodeBaseclass();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }

        }
        [HttpGet]
        [Authorize]
        [Route("GetCategory")]
        public async Task<IActionResult> GetCategory()
        {
            var result = await objgetmasterdata.GetCategoryBaseclass();

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }

        }



    }
}