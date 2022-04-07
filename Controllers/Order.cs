using System;
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
    public class Order : Controller
    {       
        DataBaseContext context;
        private IGetmasterdata objgetmasterdata;
        public Order(DataBaseContext context, IGetmasterdata objgetmasterdata)
        {
            this.objgetmasterdata = objgetmasterdata;
            this.context = context;
        }


        [HttpGet]
        [Authorize]
        [Route("GetOrderbyDate")]
        /// <param name="Salesorder_fromDate">Delivery order From Date</param>
        public async Task<IActionResult> GetOrderbyDate(string Order_fromDate, string Order_toDate)
        {
            try
            {
                DateTime f_dtime = DateTime.ParseExact(Order_fromDate, "dd-MM-yyyy", new CultureInfo("en-US"));
                DateTime t_dtime = DateTime.ParseExact(Order_toDate, "dd-MM-yyyy", new CultureInfo("en-US"));

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
        [Route("GetOrderbyOrderNo")]
        public async Task<IActionResult> GetOrderbyOrderNo(string OrderNo)
        {
            //DateTime time =DateTime.ParseExact(dateTime,"dd-MM-yyyy", new CultureInfo("en-US"));
            var result = await objgetmasterdata.getOrder(OrderNo);
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
