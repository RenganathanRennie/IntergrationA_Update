using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntergrationA.Models;
using IntergrationA.Services.Masterdetails;
using IntergrationA.Services.TaskService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApi;
using static IntergrationA.Models.barcodemodel;
using static IntergrationA.Models.categorymodel;
using static IntergrationA.Models.inventorymodel;


namespace IntergrationA.Services.Masterdetails
{
    public class Masterfile : IMasterfile
    {

        public ILogger<Masterfile> _log;
        private ILogger<RunAsyncTask> log;
        private DataBaseContext xService;
        // private readonly DataBaseContext con;
        public Masterfile(ILogger<RunAsyncTask> log, DataBaseContext xService)
        {
            this.log = log;
            this.xService = xService;

        }

        public inventorymodel.InventoryBaseclassreturn GetInventoryBaseclass()
        {
            try
            {
                var invdetails = xService.Inventory.ToList();

                List<Inventory> lst = new List<Inventory>();

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
                log.LogError(ex.Message + ex.StackTrace);
                return null;
            }
        }

        public async Task<string> getsettings(string filtervalue)
        {
            string res = string.Empty;
            try
            {
                var getsettingsvalue = await Task.Run(() => xService.settings
                .Where(o => o.settingsId == filtervalue).FirstOrDefault().settingsValue);

                if (getsettingsvalue != null)
                {
                    res = getsettingsvalue;
                }

                log.LogInformation(res);

            }
            catch (Exception ex)
            {
                _log.LogInformation(ex.Message);

            }

            return res;
        }

        public async Task<bool> insertBarcodeifnotExists(barcodebaseclass Barcodedata)
        {
            bool res = false;

            // foreach(var singldata in Barcodedata.data )
            // {
            //     // var getseq = xService.Barcode.Where(m=>m.seq==singldata.seq).FirstOrDefault();
            //     // if(getseq!=null)
            //     // {

            //     // }
            //     // else
            //     // {
            //     //     xService.Barcode.Add()
            //     // }
            // }
            try
            {
                var getrecords = xService.Barcode.ToList();
                var missingRecords = await Task.Run(() => Barcodedata.data.Where(x =>
                  !getrecords.Any(z => z.seq == x.seq)).ToList());
                if (missingRecords.Count > 0)
                {
                    List<Barcode> lstbarcode = new List<Barcode>();

                    foreach (var item in missingRecords)
                    {
                        Barcode barcodejson = new Barcode()
                        {
                            seq = item.seq,
                            cNo = "Test",
                            cMaker = "Demo",
                            ireturncount = null,
                            iswfcontrolled = null,
                            iverifystate = null,
                            UAPRuntime_RowNO = null,
                            U8CUSTDEF_0001_E001_F003 = DateTime.Now.ToString("yyyy-MM-dd"),
                            U8CUSTDEF_0001_E001_F004 = item.ItemCode,
                            U8CUSTDEF_0001_E001_F005 = item.Barcode,
                            U8CUSTDEF_0001_E001_PK = ""
                        };

                        lstbarcode.Add(barcodejson);
                    }
                    await xService.Barcode.AddRangeAsync(lstbarcode);
                    await xService.SaveChangesAsync();
                    res = true;

                }
                #region update
                var getrecordstobeupdate = xService.Barcode.ToList();
                var updaterecords = await Task.Run(() => Barcodedata.data.Where(x =>
                   !getrecordstobeupdate.Any(z => z.seq == x.seq && z.U8CUSTDEF_0001_E001_F005 == x.Barcode)).ToList());

                if (updaterecords.Count > 0)
                {
                    foreach (var item in updaterecords)
                    {

                        var getrecordstoupdate = xService.Barcode.Where(k => k.seq == item.seq).FirstOrDefault();
                        // getrecordstoupdate.U8CUSTDEF_0001_E001_F003 = DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss");
                        getrecordstoupdate.U8CUSTDEF_0001_E001_F004 = item.ItemCode;
                        getrecordstoupdate.U8CUSTDEF_0001_E001_F005 = item.Barcode;
                        xService.Barcode.Update(getrecordstoupdate);
                        await xService.SaveChangesAsync();
                    }

                    res = true;
                }
                #endregion

            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        public async Task<bool> insertCategoriesifnotExists(categorybaseclass categorybaseclass)
        {
            bool res = false;
            try
            {
                var getrecords = xService.Categories.ToList();
                var missingRecords = await Task.Run(() => categorybaseclass.data.Where(x =>
                  !getrecords.Any(z => z.seq == x.seq)).ToList());
                if (missingRecords.Count > 0)
                {
                    await xService.Categories.AddRangeAsync(missingRecords);
                    await xService.SaveChangesAsync();
                    res = true;
                }

            }
            catch (Exception)

            {
                res = false;
            }
            return res;
        }

        public async Task<bool> insertInventoryifnotExists(inventorymodel.InventoryBaseclass InventoryBaseclass)
        {
            bool res = false;
            try
            {
                var getrecords = xService.Inventory.ToList();
                var missingRecords = await Task.Run(() => InventoryBaseclass.data.Where(x =>
                  !getrecords.Any(z => z.seq == x.seq)).ToList());
                if (missingRecords.Count > 0)
                {
                    List<Inventory> lst = new List<Inventory>();
                    #region forloop


                    foreach (var item in missingRecords)
                    {

                        Inventory inv = new Inventory();

                        inv.seq = item.seq;

                        inv.itemno = item.itemno;

                        inv.stockcode = item.stockcode;

                        inv.longdescription = item.longdescription;

                        inv.ClassificationCodeL1 = item.ClassificationCode;

                        inv.ClassificationCodeL2 = item.ClassificationCode;

                        inv.ClassificationCodeL3 = item.ClassificationCode;

                        inv.originmanufacture = item.originmanufacture;

                        inv.referenceprice = item.referenceprice;

                        inv.Length = item.Length.ToString();

                        inv.Width = item.Width.ToString();

                        inv.Height = item.Height.ToString();

                        inv.Weight = item.Weight.ToString();


                        inv.Specifications = item.Specifications;

                        inv.PalletQTY = item.PalletQTY;

                        inv.PrimaryMeasurement = item.PrimaryMeasurement;

                        inv.DefaultLocation = item.DefaultLocation;

                        inv.WarehouseCode = item.WarehouseCode;

                        inv.Volume = item.Volume;

                        inv.ImageURL = item.ImageURL;

                        inv.EshopDesc = item.Eshop;

                        inv.EshopUnitPrice = item.EshopUnitPrice;

                        inv.EshopBundleQty = item.EshopBundleQty;

                        inv.EshopBundlePrice = item.EshopBundlePrice;

                        inv.MarketplaceDesc = item.Marketplace;

                        inv.MarketplacePrice = item.MarketplacePrice;

                        inv.MarketplaceBundleQty = item.MarketplaceBundleQty;

                        inv.POSDesc = item.POSDesc;

                        inv.POSBundleQTY = item.POSBundleQTY;

                        inv.POSBundleQty1 = item.POSBundleQty1;

                        inv.POSBundlePrice = item.POSBundlePrice;

                        inv.PackageWeight = item.PackageWeight;

                        inv.PackageLength = item.PackageLength;

                        inv.PackageWidth = item.PackageWidth;

                        inv.PackageHeight = item.PackageHeight;

                        inv.Eshop = item.Eshop;

                        inv.Internal = item.Internal;

                        inv.POS = item.POS;

                        inv.Marketplace = item.Marketplace;

                        inv.CreatedBy = "";

                        inv.CreatedDate = DateTime.Now;

                        inv.UpdatedBy = "";

                        inv.UpdatedDate = DateTime.Now;

                        inv.IsDeleted = false;
                        lst.Add(inv);
                    }
                    #endregion
                    await xService.Inventory.AddRangeAsync(lst);
                    await xService.SaveChangesAsync();
                    res = true;
                }
            }
            catch (Exception)

            {
                res = false;
            }
            return res;
        }
    }
}