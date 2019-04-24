using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using DataConventer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace DataConventer.Controllers
{
    public class HomeController:Controller
    {
        private int fault=0;
        private StreamWriter file;
        private IPhone[] _phones;
        private IUsers[] _users;
        private List<Employee> _employee = new List<Employee>();
        public async Task<IActionResult> Index()
        {
            //file= new StreamWriter(@"C:\Users\hp 840 g3\source\repos\DataConventer\DataConventer\wwwroot\Files\test.txt", true); // logs

            Task.WaitAll(
                Task.Run(() => { _phones = XmlConventer().Result; }),
                Task.Run(() => { _users = CsvConventer().Result; }));

           
            
            Console.WriteLine("----------------SAVE " + DateTime.Now.ToString("hh.mm.ss.fffff"));
            for (int i=0;i<_users.Count();i++)
            {
                Console.WriteLine($"{i}. " + _users[i].LastName);
                             
                        var phone = _phones.FirstOrDefault(p => p.Login.Equals(Regex.Match(_users[i].Email, @"^.*?(?=@)").Value.ToUpper()));
                        if (phone!=null)
                        {
                            _employee.Add(new Employee
                            {
                                EmployeeType = _users[i].EmployeeType,
                                Id = _users[i].Id,
                                FirstName = _users[i].FirstName,
                                LastName = _users[i].LastName,
                                Email = _users[i].Email,
                                Gender = _users[i].Gender,
                                EndDate = _users[i].EndDate,
                                Extension1 = _users[i].Extension1,
                                Login = phone.Login,
                                Phone1 = phone.Phone1,
                                Phone2 = phone.Phone2,
                                Phone3 = phone.Phone3
                            });
                        }
                        else
                        {
                            //FirstName+LastName
                            var nick = _users[i].FirstName.First() + _users[i].LastName;
                            phone = _phones.FirstOrDefault(p => p.Login.Contains(nick.ToUpper()));
                            if (phone!=null)
                            {
                                _employee.Add(new Employee
                                {
                                    EmployeeType = _users[i].EmployeeType,
                                    Id = _users[i].Id,
                                    FirstName = _users[i].FirstName,
                                    LastName = _users[i].LastName,
                                    Email = _users[i].Email,
                                    Gender = _users[i].Gender,
                                    EndDate = _users[i].EndDate,
                                    Extension1 = _users[i].Extension1,
                                    Login = phone.Login,
                                    Phone1 = phone.Phone1,
                                    Phone2 = phone.Phone2,
                                    Phone3 = phone.Phone3
                                });
                            }
                            else
                            {
                                Console.WriteLine($"-------------------------{i}. " + _users[i].LastName);
                                fault++;
                            }
                               
                        }
                
            }
            Console.WriteLine("----------------SAVE " + DateTime.Now.ToString("hh.mm.ss.fffff"));
            Console.WriteLine("Fault: "+fault);

            var action = Path.GetExtension(@"C:\Users\hp 840 g3\source\repos\DataConventer\DataConventer\wwwroot\Files\test.txt").Substring(1)+"Convert";
            var a = new Employee();
            var aaa = JsonConvert.SerializeObject(_users);
            return View("EmployeeJson",_employee);          
        }

        
        public static async Task<Users[]> CsvConventer() =>
            await Task.WhenAll(System.IO.File
                .ReadAllLines(@"C:\Users\hp 840 g3\source\repos\DataConventer\DataConventer\wwwroot\Files\users.csv")
                .Skip(1)
                .Where(line => line.Length > 1).Select(Users.ParseFromCsvAsync));

        public static async Task<Phone[]> XmlConventer() =>        
            await Task.WhenAll((from e in XDocument
                    .Load(@"C:\Users\hp 840 g3\source\repos\DataConventer\DataConventer\wwwroot\Files\phone.xml").Root
                    .Elements().Elements()
                where (string)e.Attribute("Name") == "username"
                group e.Parent by e.Value
                into g
                select g.ToList()).Select(Phone.ParseFromXmlAsync));
        //logika z ksiazki do zwracania obiektu Json do widoku 
    }
}
