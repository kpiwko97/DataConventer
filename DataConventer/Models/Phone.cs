using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataConventer.Models
{
    public class Phone:IPhone
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PhoneType { get; set; }
        public string Value { get; set; }

        public static async Task<Phone> ParseFromXmlAsync(XElement objects)
        {
            
            Console.WriteLine(DateTime.Now.ToString("hh.mm.ss.fff"));//Performance
            return new Phone
            {
                Id = "a",
                UserName = "b",
                PhoneType = "c",
                Value = "d"


                //EmployeeType = await Task.Run(() => ValidPropertyEnum<TypeOfWork>(columns[0])),
                //Id = await Task.Run(() => ValidId(columns[1])),
                //FirstName = columns[2],
                //LastName = columns[3],
                //Email = await Task.Run(() => ValidEmail(columns[4])),
                //Gender = await Task.Run(() => ValidPropertyEnum<Sex>(columns[5])),
                //EndDate = await Task.Run(() => ValidEndDate(columns[6])),
                //Extension1 = columns[7]
            };
        }

    }
}
