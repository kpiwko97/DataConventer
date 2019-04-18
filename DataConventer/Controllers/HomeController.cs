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
            var xelement =
                XDocument.Load(@"C:\Users\hp 840 g3\source\repos\DataConventer\DataConventer\wwwroot\Files\phone.xml");
            
            var list1 = xelement.Root.Elements();
            
            //var action = Path.GetExtension(@"C:\Users\hp 840 g3\source\repos\DataConventer\DataConventer\wwwroot\Files\test.txt").Substring(1)+"Convert";

           /* var homePhone = xelement.Elements();*/ // all elements

            var  address =
                from el in list1.Elements()
                where (string)el.Attribute("Name") == "id"
                select el.Value;
            var aaa = from element in list1.Elements()
                where element.Value == "vjWJem2qcU+cxPT2qIvqsw=="
                      select element;

            var aaaa = from e in list1.Elements() where (string)e.Attribute("Name") == "username" group e.Parent by e.Value into g  select  new { PersonId = g.Key, Cars = g.ToList() };
            var aa = 0;
            //foreach (var item in homePhone1)
            //{             
            //    var elemList = item.Elements();
            //    //zrob z tego funkcje void ReturnTableofContents ( Attribute, Value ) { return Add.ListOfAll }
            //    string Username = elemList.Single(phoneElement => phoneElement.Attribute("Name")?.Value == "username")?.Value;
            //    // logika walidacji
            //    file.WriteLine(Username);
            //    string Phonetype = elemList.Single(phoneElement => phoneElement.Attribute("Name")?.Value == "phoneType")?.Value;
            //    string Value = elemList.Single(phoneElement => phoneElement.Attribute("Name")?.Value == "value")?.Value;
            //    string Id = elemList.Single(phoneElement => phoneElement.Attribute("Name")?.Value == "id")?.Value;
            //    // nastepnie dodaj do listy kolekcji           
            //}
            //file.Close();
            return View("UsersCsv", CsvConventer().Result);
            return View("PhoneXml",XmlConventer().Result);
            
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

        public static async Task<Phone[]> XmlConventer() =>
            await Task.WhenAll(XDocument
                .Load(@"C:\Users\hp 840 g3\source\repos\DataConventer\DataConventer\wwwroot\Files\phone.xml").Root
                .Elements().Select(Phone.ParseFromXmlAsync));

    }
}
