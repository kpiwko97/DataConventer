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
            XElement xelement = XElement.Load(@"C:\Users\hp 840 g3\source\repos\DataConventer\DataConventer\wwwroot\Files\phone.xml");
            var emploeyess = CsvConventer();

            var action = Path.GetExtension(@"C:\Users\hp 840 g3\source\repos\DataConventer\DataConventer\wwwroot\Files\test.txt").Substring(1)+"Convert";

            var homePhone = xelement.Elements(); // all elements
            var homePhone1 = from c in xelement.Elements() select c; // group of Object
            var homePhone2 = from c in xelement.Element("Object").Elements() select c; // single properties


            foreach (var item in homePhone1)
            {
                
                var elemList = item.Elements();
                //zrob z tego funkcje void ReturnTableofContents ( Attribute, Value ) { return Add.ListOfAll }
                string Username = elemList.Single(phoneElement => phoneElement.Attribute("Name")?.Value == "username")?.Value;
                // logika walidacji
                file.WriteLine(Username);
                string Phonetype = elemList.Single(phoneElement => phoneElement.Attribute("Name")?.Value == "phoneType")?.Value;
                string Value = elemList.Single(phoneElement => phoneElement.Attribute("Name")?.Value == "value")?.Value;
                string Id = elemList.Single(phoneElement => phoneElement.Attribute("Name")?.Value == "id")?.Value;
                // nastepnie dodaj do listy kolekcji           
            }
            file.Close();
            //return RedirectToAction("");
            return RedirectToAction(action);
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

        public static async Task<List<Users>> CsvConventer()
        {
            await Task.Run(() => System.IO.File
                .ReadAllLines(@"C:\Users\hp 840 g3\source\repos\DataConventer\DataConventer\wwwroot\Files\users.csv")
                .Where(line => line.Length > 1).Select(Users.ParseFromCsvAsync)
                .ToList());
            return null; 
        }


    }
}
