using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PFM.Application.Result
{
    
    [JsonDerivedType(typeof(ValidationError), nameof(ValidationError))]
    [JsonDerivedType(typeof(ServerError), nameof(ServerError))]
    [JsonDerivedType(typeof(BusinessError), nameof(BusinessError))]
    public abstract class Error
    {
    }
}
