using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntergrationA.Services.TaskService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApi;
using static IntergrationA.Models.barcodemodel;

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

            }
            catch (Exception)

            {
                res = false;
            }
            return res;
        }
    }
}