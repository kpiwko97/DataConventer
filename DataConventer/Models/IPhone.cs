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
        string Phone1 { get; set; }
        string Phone2 { get; set; }
        string Phone3 { get; set; }
    }

}
