using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebApi;

namespace IntergrationA.Services.Masterdetails
{
    public class Masterfile : IMasterfile
    {
        private readonly ILogger<Masterfile> _log;
        private readonly DataBaseContext con;

        public Masterfile(ILogger<Masterfile> _log,DataBaseContext con)
        {
            this._log=_log;
            this.con = con;
        }
        public async Task<string> getsettings(string filtervalue)
        {
            string res = string.Empty;
            try
            {
                var getsettingsvalue =await Task.Run(()=> con.SLBAdminUser.Where(o=>o.MobileNo=="test").FirstOrDefault().Status);

            }
            catch(Exception ex)
            {
            _log.LogInformation(ex.Message);

            }

            return res;
        }
    }
}