using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Application.UseCases.Result
{
    public class ValidationError : Error
    {
        public string Tag { get; set; }

        public string Error { get; set; }
        public string Message { get; set; }
    }
}
