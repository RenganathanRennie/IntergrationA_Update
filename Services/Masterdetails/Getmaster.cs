
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntergrationA.Models;
using IntergrationA.Services.TaskService;
using IntergrationA_Update.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApi;
using static IntergrationA.Models.barcodemodel;
using static IntergrationA.Models.categorymodel;
using static IntergrationA.Models.inventorymodel;
using static IntergrationA_Update.Models.domodel;
using static IntergrationA_Update.Models.somodel;

namespace IntergrationA_Update.Services.Masterdetails
{
    public class Getmasterdata : IGetmasterdata
    {
        DataBaseContext xService;

        public Getmasterdata(DataBaseContext xService)
        {
            this.xService = xService;
        }

        public async Task<barcodebaseclass> GetBarcodeBaseclass()
        {
            try
            {
                var invdetails = await Task.Run(() => xService.Barcode.ToList());

                List<barcodejson> _barcodejson = await convertBcodeList(invdetails);
                if (invdetails.Count > 0)
                {
                    barcodebaseclass invbase = new barcodebaseclass()
                    {
                        Success = true,
                        Message = "200",
                        data = _barcodejson
                    };

                    return invbase;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                //log.LogError(ex.Message + ex.StackTrace);
                return null;
            }
        }

        private async Task<List<barcodejson>> convertBcodeList(List<Barcode> invdetails)
        {
            List<barcodejson> lstbcode = new List<barcodejson>();
            await Task.Run(() =>
            {
                foreach (var item in invdetails)
                {
                    barcodejson bcode = new barcodejson();
                    bcode.Barcode = item.U8CUSTDEF_0001_E001_F005;
                    bcode.seq = item.seq;
                    bcode.ItemCode = item.U8CUSTDEF_0001_E001_F004;
                    lstbcode.Add(bcode);

                }

            });
            return lstbcode;
        }

        public async Task<categorybaseclass> GetCategoryBaseclass()
        {
            try
            {
                var invdetails = await Task.Run(() => xService.Categories.ToList());
                if (invdetails.Count > 0)
                {
                    categorybaseclass invbase = new categorybaseclass()
                    {
                        Success = true,
                        Message = "200",
                        data = invdetails
                    };

                    return invbase;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                //log.LogError(ex.Message + ex.StackTrace);
                return null;
            }
        }

        public async Task<InventoryBaseclassreturn> GetInventoryBaseclass()
        {
            try
            {
                var invdetails = await Task.Run(() => xService.Inventory.ToList());
                if (invdetails.Count > 0)
                {
                    InventoryBaseclassreturn invbase = new InventoryBaseclassreturn()
                    {
                        Success = true,
                        Message = "200",
                        data = invdetails
                    };

                    return invbase;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                //log.LogError(ex.Message + ex.StackTrace);
                return null;
            }
        }

        public async Task<string> getdo(DateTime filter, DateTime filter2)
        {
            try
            {
                // var invdetails = await Task.Run(() => (from dod in xService.OrderDetails
                //                                        join doh in xService.OrderHeader
                //                                        on dod.OrderNo equals doh.OrderNo
                //                                        where dod.OrderNo == doh.OrderNo && (doh.DoDate.Date >= filter.Date
                //                                         && doh.DoDate.Date <= filter2)
                //                                        select new
                //                                        { Orderheader = doh, Orderdetails = dod }).ToList()

                // );
                // if (invdetails.Count > 0)
                // {
                //     var jsondata = JsonConvert.SerializeObject(invdetails);
                //     return jsondata;
                // }
                // else
                // {
                //     return null;
                // }
                List<somodelsummary> lstdomodelsum = new List<somodelsummary>();
                var Orderhader = await Task.Run(() => (xService.SalesOrderHeader.Where(x => x.SalesOrderDate >= filter.Date && x.SalesOrderDate.Date <= filter2).ToList()));
                if (Orderhader.Count() > 0)
                {
                    foreach (var item in Orderhader)
                    {
                        var Orderde = await Task.Run(() => (xService.SalesOrderDetails.Where(x => x.SalesOrderNo == item.SalesOrderNo).ToList()));

                        somodelsummary sum = new somodelsummary()
                        {
                            sodetails = Orderde,
                            soheader = item
                        };

                        lstdomodelsum.Add(sum);
                    }

                }
                if (lstdomodelsum.Count > 0)
                {
                    var jsondata = JsonConvert.SerializeObject(lstdomodelsum);
                    return jsondata;
                }
                else
                {
                    return null;
                }



            }
            catch (Exception ex)
            {
                //log.LogError(ex.Message + ex.StackTrace);
                return null;
            }

        }
        public async Task<string> getdo(string filter)
        {
            try
            {
                // var invdetails = await Task.Run(() => (from doh in xService.OrderHeader
                //                                        join dod in xService.OrderDetails
                //                                        on doh.OrderNo equals dod.OrderNo

                //                                        select new
                //                                        { Orderheader = doh, Orderdetails = dod })

                // );
                var Orderhader = await Task.Run(() => (xService.SalesOrderHeader.Where(x => x.SalesOrderNo == filter).FirstOrDefault()));
                var Orderde = await Task.Run(() => (xService.SalesOrderDetails.Where(x => x.SalesOrderNo== Orderhader.SalesOrderNo).ToList()));
                if (Orderhader != null && Orderde != null)
                {
                    somodelsummary sum = new somodelsummary()
                    {
                        sodetails = Orderde,
                        soheader = Orderhader
                    };

                    
                    var hjsondata = JsonConvert.SerializeObject(sum);
                    //var jsonresult = JsonConvert.DeserializeObject<domodelsummary>(hjsondata);
                    return hjsondata;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                //log.LogError(ex.Message + ex.StackTrace);
                return null;
            }

        }

        public async Task<bool> postdo(domodel.domodelsummary domodelsummary)
        {
            bool res = false;
            try
            {

                await xService.DeliveryOrderDetails.AddRangeAsync(domodelsummary.dodetails);
                await xService.DeliveryOrderHeader.AddAsync(domodelsummary.doheader);
                await xService.SaveChangesAsync();
                res = true;
                return res;

            }
            catch (Exception ex)
            {
                //log.LogError(ex.Message + ex.StackTrace);
                return res;
            }
        }
    }
}