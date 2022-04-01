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
    public class InterfaceData : ControllerBase
    {
        DataBaseContext context;
        private IGetmasterdata objgetmasterdata;
        public InterfaceData(DataBaseContext context, IGetmasterdata objgetmasterdata)
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

        [HttpGet]
        [Authorize]
        [Route("GetDobyDate")]

        /// <param name="deliveryorder_fromDate">Delivery order From Date</param>
        public async Task<IActionResult> GetDobyData(string deliveryorder_fromDate, string deliveryorder_toDate)
        {
            try
            {
                DateTime f_dtime = DateTime.ParseExact(deliveryorder_fromDate, "dd-MM-yyyy", new CultureInfo("en-US"));
                DateTime t_dtime = DateTime.ParseExact(deliveryorder_toDate, "dd-MM-yyyy", new CultureInfo("en-US"));

                var result = await objgetmasterdata.getdo(f_dtime, t_dtime.AddHours(23).AddMinutes(59).AddSeconds(59));
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }
            catch
            {
                return BadRequest();

            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetDobyOrderNo")]
        public async Task<IActionResult> GetDobyOrderNo(string OrderNo)
        {
            //DateTime time =DateTime.ParseExact(dateTime,"dd-MM-yyyy", new CultureInfo("en-US"));
            var result = await objgetmasterdata.getdo(OrderNo);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        [Authorize]
        [Route("PostDo")]
        public async Task<IActionResult> PostDo([FromBody] JsonElement domodelsummary)
        {
            try
            {
                var getstringfrombody = domodelsummary.GetRawText();

                var dm = JsonConvert.DeserializeObject<domodelsummary>(getstringfrombody);
                if (dm != null)
                {

                    if (this.ModelState.IsValid)
                    {
                        //DateTime time =DateTime.ParseExact(dateTime,"dd-MM-yyyy", new CultureInfo("en-US"));
                        var result =await objgetmasterdata.postdo(dm);
                       // var result = false;
                        if (result)
                        {
                            return Ok(" DO Success");
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                    else
                    {
                        return BadRequest("Missing Required Fields");
                    }
                }
                else
                {
                    return BadRequest("Missing Required Fields");

                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }





        }
    }
}