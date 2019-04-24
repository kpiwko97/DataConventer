using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataConventer.Models
{
    public class Employee:IPhone,IUsers
    {
        public TypeOfWork EmployeeType { get; set; }
        public string Id { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Sex Gender { get; set; }
        public string EndDate { get; set; }
        public string Extension1 { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
    }
}
