using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Application.UseCases.Result
{
    public class ServerError : Error
    {
        public string Message { get; set; }
    }
}
