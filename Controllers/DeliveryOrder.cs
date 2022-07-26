using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Threading.Tasks;
using IntergrationA_Update.Services.Masterdetails;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi;
using static IntergrationA_Update.Models.domodel;

namespace WebApi.Controllers
{
    [ApiController]
    public class DeliveryOrder : Controller
    {
        DataBaseContext context;
        private IGetmasterdata objgetmasterdata;
        public DeliveryOrder(DataBaseContext context, IGetmasterdata objgetmasterdata)
        {
            this.objgetmasterdata = objgetmasterdata;
            this.context = context;
        }


        //DO

        [HttpGet]
        [Authorize]
        [Route("GetDObyDate")]
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
        [Route("GetDObyOrderNo")]
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
        [Route("PostDO")]
        public async Task<IActionResult> PostDo([FromBody]deliveryOrder deliveryOrder)
        {
            try
            {
               // var getstringfrombody = domodelsummary.GetRawText();

               // var dm = JsonConvert.DeserializeObject<domodelsummary>(getstringfrombody);
                if (deliveryOrder != null)
                {

                    if (this.ModelState.IsValid)
                    {
                        
                        //DateTime time =DateTime.ParseExact(dateTime,"dd-MM-yyyy", new CultureInfo("en-US"));
                        var result = await objgetmasterdata.postdo(deliveryOrder);
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
