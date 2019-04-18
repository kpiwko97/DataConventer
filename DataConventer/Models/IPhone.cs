using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataConventer.Models
{
    interface IPhone
    {
        string Id { get; set; }
        string UserName { get; set; }
        string PhoneType { get; set; }
        string Value { get; set; }
    }

}
