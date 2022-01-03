using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntergrationA.Services.TaskService;
using Microsoft.Extensions.Logging;
using WebApi;

namespace IntergrationA.Services.Masterdetails
{
    public class Masterfile : IMasterfile
    {
        public ILogger<Masterfile> _log;
        private ILogger<RunAsyncTask> log;
        private DataBaseContext xService;
        private readonly DataBaseContext con;
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

                if(getsettingsvalue!=null)
                {
                    res=getsettingsvalue;
                }

                log.LogInformation(res);

            }
            catch (Exception ex)
            {
                _log.LogInformation(ex.Message);

            }

            return res;
        }
    }
}