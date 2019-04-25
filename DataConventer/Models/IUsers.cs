using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataConventer.Models
{

    public interface IUsers
    {
         TypeOfWork EmployeeType { get; set; }
         string Id { get; set; }
         string FirstName { get; set; }
         string LastName { get; set; }
         string Email { get; set; }
         Sex Gender { get; set; }
         string EndDate { get; set; }
         string Extension1 { get; set; }
    }
     public enum TypeOfWork
    {
        WrongType,
        Contractor,
        FullTime,
    }
     public enum Sex
    {
        WrongType,
        Male,
        Female
    }
}
