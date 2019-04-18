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
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }

        public static async Task<Phone> ParseFromXmlAsync(List<XElement> objects)
        {
            
            Console.WriteLine(DateTime.Now.ToString("hh.mm.ss.fff"));//Performance

            return new Phone
            {
                Id = await Task.Run(()=>objects[0].Elements().Single(phoneElement => phoneElement.Attribute("Name")?.Value == "id").Value),
                UserName = await Task.Run(() => objects[0].Elements().Single(phoneElement => phoneElement.Attribute("Name")?.Value == "username").Value),
                Phone1 = await Task.Run(() => objects[0].Elements().Single(phoneElement => phoneElement.Attribute("Name")?.Value == "value").Value),
                Phone2 = await Task.Run(() =>objects.Count()>1?objects[1].Elements().Single(phoneElement => phoneElement.Attribute("Name")?.Value == "value").Value:String.Empty),
                Phone3 = await Task.Run(() => objects.Count() > 2 ? objects[2].Elements().Single(phoneElement => phoneElement.Attribute("Name")?.Value == "value").Value : String.Empty),
            };
        }

    }
}
