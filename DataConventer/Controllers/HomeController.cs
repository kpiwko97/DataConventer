using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using DataConventer.Models;
using Microsoft.AspNetCore.Mvc;


namespace DataConventer.Controllers
{
    public class HomeController:Controller
    {
        private StreamWriter file;
        public IActionResult Index()
        {
            file= new StreamWriter(@"C:\Users\hp 840 g3\source\repos\DataConventer\DataConventer\wwwroot\Files\test.txt", true); // logs

            //var action = Path.GetExtension(@"C:\Users\hp 840 g3\source\repos\DataConventer\DataConventer\wwwroot\Files\test.txt").Substring(1)+"Convert";
      

            return View("PhoneXml", XmlConventer().Result);
            return View("UsersCsv", CsvConventer().Result);        
            
        }

        public IActionResult UploadFile()
        {
            return null;
        }
        public ViewResult CsvConvert()
        {
            return View("Index");
        }
        public ViewResult TxtConvert()
        {
            
            return View("Index");
        }
        


        public static async Task<Users[]> CsvConventer() =>
            await Task.WhenAll(System.IO.File
                .ReadAllLines(@"C:\Users\hp 840 g3\source\repos\DataConventer\DataConventer\wwwroot\Files\users.csv")
                .Skip(1)
                .Where(line => line.Length > 1).Select(Users.ParseFromCsvAsync));

        public static async Task<Phone[]> XmlConventer()
        {
            return await Task.WhenAll((from e in XDocument
                    .Load(@"C:\Users\hp 840 g3\source\repos\DataConventer\DataConventer\wwwroot\Files\phone.xml").Root
                    .Elements().Elements()
                where (string)e.Attribute("Name") == "username"
                group e.Parent by e.Value
                into g
                select g.ToList()).Select(Phone.ParseFromXmlAsync));
        }


    }
}
