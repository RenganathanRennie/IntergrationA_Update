using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntergrationA.Models;
using IntergrationA.Services.TaskService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApi;
using static IntergrationA.Models.barcodemodel;
using static IntergrationA.Models.categorymodel;

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
                    await xService.Inventory.AddRangeAsync(missingRecords);
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