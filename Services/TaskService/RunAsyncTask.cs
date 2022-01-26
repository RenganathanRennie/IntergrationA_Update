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
                await Task.Delay(2000);
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

                var deseraile = JsonConvert.DeserializeObject<Root>(await getjsondata("set002"));
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

                var deseraile = JsonConvert.DeserializeObject<barcodebaseclass>(await getjsondata("set003"));

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
                            if(res)
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