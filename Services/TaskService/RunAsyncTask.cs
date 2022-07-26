using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.Threading;
using Microsoft.Extensions.Logging;
using WebApi;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using IntergrationA.Models;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using Newtonsoft.Json.Linq;
using static IntergrationA.Models.inventorymodel;
using Newtonsoft.Json;
using IntergrationA.Services.Masterdetails;
using static IntergrationA.Models.barcodemodel;
using static IntergrationA.Models.categorymodel;
using ShopifySharp;
using System.Collections.Generic;
using ShopifySharp.Filters;
using static WebApi.Models.shopify_model;
using System.Text.RegularExpressions;
using static IntergrationA_Update.Models.somodel;

namespace IntergrationA.Services.TaskService
{
    public class RunAsyncTask : BackgroundService
    {
        public ILogger<RunAsyncTask> _log;

        private readonly IServiceScopeFactory _scopeFactory;
        HttpClient client = new HttpClient();
        public RunAsyncTask(ILogger<RunAsyncTask> _log, IServiceScopeFactory _scopeFactory)
        {
            this._log = _log;
            this._scopeFactory = _scopeFactory;

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _log.LogInformation("Loop ran at " + DateTime.Now.ToString());
                 await GetInventoryfile(_log);
                 await Getcustomerfile(_log);
                 await GetBarcodefile(_log);
                 await GetCategoryfile(_log);       

                await getshopifyorder();
                await Task.Delay(10000);
            }
        }

        private async Task getshopifyorder()
        {
            string refsono = "";
            //int count =0;
            try
            {

                var allOrders = new List<Order>();
                var service = new OrderService("toyogo-supermart.myshopify.com", "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");
                var orders = await service.ListAsync();
                var orderdes = JsonConvert.SerializeObject(orders.Items);
                var myDeserializedClass = JsonConvert.DeserializeObject<List<MyArray>>(orderdes);

                double strgstamount = 0;
                foreach (var shopifyrawlist in myDeserializedClass)
                {


                    int count = 0;
                    string checkorderexists = "";
                    List<SalesOrderDetails> lstsalesOrderDetails = new List<SalesOrderDetails>();
                    refsono = shopifyrawlist.order_number.ToString();

                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var xService = scope.ServiceProvider.GetService<DataBaseContext>();
                        checkorderexists = Convert.ToString(xService.SalesOrderHeader.Where(o => o.SalesOrderNo == refsono).Select(op=>op.SalesOrderNo).FirstOrDefault());
                    }
                    if (checkorderexists == "" || checkorderexists==null)
                    {
                        SalesOrderHeader Soheader = new SalesOrderHeader
                        {
                            SalesOrderType = "Shopify",
                            SalesOrderNo = shopifyrawlist.order_number.ToString(),
                            SalesOrderDate = shopifyrawlist.created_at,
                            InvoiceNo = "",
                            InvoiceDate = DateTime.Now.Date,
                            DoNo = "",
                            DoDate = DateTime.Now.Date,
                            CustomerId = shopifyrawlist.customer.id.ToString(),
                            Customer_FirstName = shopifyrawlist.shipping_address is null ? "" : shopifyrawlist.shipping_address.first_name,
                            Customer_LastName = shopifyrawlist.shipping_address is null ? "" : shopifyrawlist.shipping_address.last_name,
                            ShippingCode = shopifyrawlist.shipping_address is null ? "" : Convert.ToString(shopifyrawlist.shipping_address.id),
                            ShippingAddress1 = shopifyrawlist.shipping_address is null ? "" : shopifyrawlist.shipping_address.address1,
                            ShippingAddress2 = shopifyrawlist.shipping_address is null ? "" : shopifyrawlist.shipping_address.address2,
                            ShippingAddress3 = "",
                            ShippingZipcode = shopifyrawlist.shipping_address is null ? "" : shopifyrawlist.shipping_address.zip,
                            ShippingPhone = shopifyrawlist.shipping_address is null ? "" : shopifyrawlist.shipping_address.phone,
                            ShippingCost = shopifyrawlist.total_shipping_price_set.shop_money == null ? 0 : shopifyrawlist.total_shipping_price_set.shop_money.amount,
                            // ShippingMode = "",
                            Total = shopifyrawlist.total_price,
                            DiscountType = "",
                            DiscountPer = 0,
                            DiscountAmount = shopifyrawlist.current_total_discounts,
                            TotalBillDiscount = shopifyrawlist.total_discounts,
                            TotalItemDiscount = 0,
                            SubTotal = shopifyrawlist.subtotal_price,
                            GSTType = "",
                            GSTPer = 7,
                            GSTAmount = shopifyrawlist.total_tax,
                            NetTotal = shopifyrawlist.subtotal_price,
                            Currency = shopifyrawlist.currency,
                            CurrencyRate = 1,
                            PaidAmount = 0,
                            Due = 0,
                            Change = 0,
                            Remarks = "",
                            PaymentNo = "",
                            PaymentType = "",
                            PaymentMode = "",
                            PaymentNotes = "",
                            CreatedBy = shopifyrawlist.user_id is null ? "NA" : shopifyrawlist.user_id.ToString(),
                            CreatedDate = shopifyrawlist.created_at,
                            UpdatedBy = "",
                            UpdatedDate = DateTime.Now.Date,
                            IsDeleted = 0
                        };

                        foreach (var line_items in shopifyrawlist.line_items)
                        {
                            count = count + 1;
                            foreach (var taxitem in shopifyrawlist.tax_lines)
                            {
                                strgstamount = taxitem.price;
                            }
                            SalesOrderDetails salesOrderDetails = new SalesOrderDetails
                            {

                               // slno = count,
                               slno=0,
                                SalesOrderNo = Convert.ToString(shopifyrawlist.order_number),
                                ProductId = Convert.ToString(line_items.product_id),
                                ProductName = Convert.ToString(line_items.title),
                                UnitId = Convert.ToString(line_items.product_id),
                                Quantity = line_items.quantity,
                                Price = Convert.ToDecimal(line_items.price),
                                Discount = Convert.ToDecimal(line_items.total_discount),
                                DiscountPer = 0,
                                Total = Convert.ToDecimal(line_items.quantity * line_items.price),
                                Remarks = "",
                                GSTType = "",
                                GSTAmount = Convert.ToDecimal(strgstamount),
                                CreatedBy = shopifyrawlist.user_id is null ? "NA" : shopifyrawlist.user_id.ToString(),
                                CreatedDate = shopifyrawlist.created_at,
                                UpdatedBy = "",
                                UpdatedDate = DateTime.Now.Date,
                                IsDeleted = false

                            };
                            lstsalesOrderDetails.Add(salesOrderDetails);
                        }
                        using (var scope = _scopeFactory.CreateScope())

                        {
                            var xService = scope.ServiceProvider.GetService<DataBaseContext>();

                            IMasterfile sim = new Masterfile(_log, xService);
                            var res = await sim.getsofromshopify(Soheader, lstsalesOrderDetails);
                            if (res)
                            {
                                _log.LogInformation("shopify order " + refsono + " Saved Success");
                            }
                            else
                            {
                                _log.LogInformation("shopify order " + refsono + " Error in saving ");
                            }

                        }
                    }
                }



            }
            catch (Exception ex)
            {
                _log.LogInformation("shopify order " + refsono + " Error in saving ");

            }
        }

        private async Task GetCategoryfile(ILogger<RunAsyncTask> log)
        {
            try
            {

                var deseraile = JsonConvert.DeserializeObject<categorybaseclass>(await getjsondata("set004"));
                if (deseraile.Success == true)
                {
                    if (deseraile.data.Count > 0)
                    {

                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var xService = scope.ServiceProvider.GetService<DataBaseContext>();

                            IMasterfile sim = new Masterfile(_log, xService);
                            var res = await sim.insertCategoriesifnotExists(deseraile);
                            if (res)
                            {
                                _log.LogInformation("Category Saved Success");
                            }


                        }
                    }
                }

                else
                {
                    _log.LogCritical(deseraile.Message);
                }

            }

            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                _log.LogTrace(ex.StackTrace);

            }

        }

        public async Task<string> getjsondata(string filter)
        {
            string result = string.Empty;

            using (var scope = _scopeFactory.CreateScope())
            {
                var xService = scope.ServiceProvider.GetService<DataBaseContext>();
                try
                {
                    IMasterfile sim = new Masterfile(_log, xService);
                    string getvalue = await sim.getsettings(filter);
                    //HttpResponseMessage response = await client.GetAsync("http://151.192.64.18:6154/U8Interface/U8Api.asmx/GetBasicFile?content={\"Login\":{\"UserName\":\"demo\",\"UserPassword\":\"123\",\"AccID\":\"001\",\"LoginDate\":\"2021-05-01\"},\"filetype\":\"inventory\",\"filecode\":\"\",\"fields\":\"t.cinvcode as itemno,t.cInvCode as stockcode ,cidefine15 as longdescription ,cinvccode as ClassificationCode,cEnterprise as originmanufacture,cInvDefine4 as referenceprice ,fLength as Length,fWidth as Width,fHeight as Height,fGrossW as Weight,cInvStd as Specifications,cInvDefine2 as PalletQTY,cComUnitCode as PrimaryMeasurement,cPosition as DefaultLocation,cDefWareHouse as WarehouseCode,cInvDefine1 as Volume,cInvDefine7 as ImageURL,cInvDefine10 as EshopDesc,cInvDefine14 as EshopUnitPrice,cInvDefine12 as EshopBundleQty,cInvDefine13 as EshopBundlePrice,cidefine2 as MarketplaceDesc,cidefine3 as MarketplacePrice,cidefine4 as MarketplaceBundleQty,cidefine6 as POSDesc ,cidefine7 as POSBundleQTY,cidefine8 as POSBundleQty,cidefine9 as POSBundlePrice,cidefine14 as PackageWeight,cidefine11 as PackageLength,cidefine12 as PackageWidth,cidefine13 as PackageHeight,cinvdefine5 as Eshop,cidefine10 as Internal,cidefine5 as POS,cidefine1 as Marketplace\",\"PageSize\":\"20\",\"CurrPage\":\"0\"}");
                    HttpResponseMessage response = await client.GetAsync(getvalue);
                    if (response.IsSuccessStatusCode)
                    {
                        var res = await response.Content.ReadAsStringAsync();
                        _log.LogInformation(res);
                        var getelements = XElement.Parse(res);
                        XElement xElement = new XElement(getelements.Name.LocalName);
                        xElement.Value = getelements.Value;
                        result = xElement.Value;
                        //var deseraile = JsonConvert.DeserializeObject<Root>(valuefromxml);

                    }
                }
                catch (System.Exception ex)
                {

                    _log.LogError(ex.Message);
                    _log.LogTrace(ex.StackTrace);
                }
            }

            return result;

        }
        private async Task Getcustomerfile(ILogger<RunAsyncTask> log)
        {

            try
            {
                // using (var scope = _scopeFactory.CreateScope())
                // {
                //     var context = scope.ServiceProvider.GetRequiredService<DataBaseContext>();                  
                //     //do what you need
                // }





                // HttpResponseMessage response = await client.GetAsync("http://151.192.64.18:6154/U8Interface/U8Api.asmx/GetBasicFile?content={\"Login\":{\"UserName\":\"demo\",\"UserPassword\":\"123\",\"AccID\":\"001\",\"LoginDate\":\"2021-05-01\"},\"filetype\":\"inventory\",\"filecode\":\"\",\"fields\":\"t.cinvcode as itemno,t.cInvCode as stockcode ,cidefine15 as longdescription ,cinvccode as ClassificationCode,cEnterprise as originmanufacture,cInvDefine4 as referenceprice ,fLength as Length,fWidth as Width,fHeight as Height,fGrossW as Weight,cInvStd as Specifications,cInvDefine2 as PalletQTY,cComUnitCode as PrimaryMeasurement,cPosition as DefaultLocation,cDefWareHouse as WarehouseCode,cInvDefine1 as Volume,cInvDefine7 as ImageURL,cInvDefine10 as EshopDesc,cInvDefine14 as EshopUnitPrice,cInvDefine12 as EshopBundleQty,cInvDefine13 as EshopBundlePrice,cidefine2 as MarketplaceDesc,cidefine3 as MarketplacePrice,cidefine4 as MarketplaceBundleQty,cidefine6 as POSDesc ,cidefine7 as POSBundleQTY,cidefine8 as POSBundleQty,cidefine9 as POSBundlePrice,cidefine14 as PackageWeight,cidefine11 as PackageLength,cidefine12 as PackageWidth,cidefine13 as PackageHeight,cinvdefine5 as Eshop,cidefine10 as Internal,cidefine5 as POS,cidefine1 as Marketplace\",\"PageSize\":\"20\",\"CurrPage\":\"0\"}");
                // if (response.IsSuccessStatusCode)
                // {

                //     var res = await response.Content.ReadAsStringAsync();
                //     log.LogInformation(res);

                //     var getelements = XElement.Parse(res);
                //     XElement xElement = new XElement(getelements.Name.LocalName);
                //     xElement.Value = getelements.Value;
                //     string valuefromxml = xElement.Value;
                //     var deseraile = JsonConvert.DeserializeObject<Root>(valuefromxml);
                // }

                //var deseraile = JsonConvert.DeserializeObject<Root>(await getjsondata("set002"));
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
            }
        }

        private async Task GetInventoryfile(ILogger<RunAsyncTask> log)
        {
            try
            {

                var deseraile = JsonConvert.DeserializeObject<InventoryBaseclass>(await getjsondata("set002"));
                if (deseraile.Success == true)
                {
                    if (deseraile.data.Count > 0)
                    {

                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var xService = scope.ServiceProvider.GetService<DataBaseContext>();

                            IMasterfile sim = new Masterfile(_log, xService);
                            var res = await sim.insertInventoryifnotExists(deseraile);
                            if (res)
                            {
                                _log.LogInformation("Inventory Saved Success");
                            }


                        }
                    }
                }

                else
                {
                    _log.LogCritical(deseraile.Message);
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                _log.LogTrace(ex.StackTrace);

            }


        }
        private async Task GetBarcodefile(ILogger<RunAsyncTask> log)
        {
            try
            {

                var deseraile = JsonConvert.DeserializeObject<barcodebaseclass>(await getjsondata("set003"));
                if (deseraile.Success == true)
                {
                    if (deseraile.data.Count > 0)
                    {

                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var xService = scope.ServiceProvider.GetService<DataBaseContext>();

                            IMasterfile sim = new Masterfile(_log, xService);
                            var res = await sim.insertBarcodeifnotExists(deseraile);
                            if (res)
                            {
                                _log.LogInformation("Barcode Saved Success");
                            }


                        }
                    }
                }

                else
                {
                    _log.LogCritical(deseraile.Message);
                }

            }

            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                _log.LogTrace(ex.StackTrace);

            }


        }
    }
}