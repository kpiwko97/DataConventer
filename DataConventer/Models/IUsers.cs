using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataConventer.Models
{
    public interface IUsers
    {
        string Id { get; }
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
        string EndDate { get; }
        string Extension1 { get; }
    }
}
