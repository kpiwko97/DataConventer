using DataConventer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace DataConventer.Controllers
{
    public class HomeController:Controller
    {     
        private StreamWriter _logs;
        private IPhone[] _phones;
        private IUsers[] _users;
        private IEnumerable<Employee> _employees;

        public IActionResult Index()
        {
            _logs = new StreamWriter(@"C:\Users\hp 840 g3\source\repos\DataConventer\DataConventer\wwwroot\Files\logs.txt", true);

            Task.WaitAll(
                Task.Run(() => { _phones = XmlConverterAsync().Result; }),
                Task.Run(() => { _users = CsvConverterAsync().Result; }));
            _employees = JsonConverter();
            _logs.Close();
            //return View("EmployeeJson", JsonConvert.SerializeObject(_employees));
            return View("EmployeeIEnumerable",_employees);
        }
          
        
        public static async Task<Users[]> CsvConverterAsync() =>
            await Task.WhenAll(System.IO.File
                .ReadAllLines(@"C:\Users\hp 840 g3\source\repos\DataConventer\DataConventer\wwwroot\Files\users.csv")
                .Skip(1)
                .Where(line => line.Length > 1).Select(Users.ParseFromCsvAsync));

        public static async Task<Phone[]> XmlConverterAsync() =>        
            await Task.WhenAll((from p in XDocument
                    .Load(@"C:\Users\hp 840 g3\source\repos\DataConventer\DataConventer\wwwroot\Files\phone.xml").Root
                    .Elements().Elements()
                where (string)p.Attribute("Name") == "username"
                group p.Parent by p.Value
                into g
                select g.ToList()).Select(Phone.ParseFromXmlAsync));

        public IEnumerable<Employee> JsonConverter() =>
            from p in _phones
            from u in _users
            where p.Login.Equals(Regex.Match(u.Email, @"^.*?(?=@)").Value.ToUpper()) ||
                  p.Login.Contains((u.FirstName.First() + u.LastName).ToUpper())
            select new Employee()
            {
                EmployeeType = u.EmployeeType,
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Gender = u.Gender,
                EndDate = u.EndDate,
                Extension1 = u.Extension1,
                Login = p.Login,
                Phone1 = p.Phone1,
                Phone2 = p.Phone2,
                Phone3 = p.Phone3
            };
    }
}
