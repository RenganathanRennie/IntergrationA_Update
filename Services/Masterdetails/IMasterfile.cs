using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntergrationA.Services.Masterdetails
{
    public interface IMasterfile
    {
       Task<string> getsettings(string filtervalue);

    }
}