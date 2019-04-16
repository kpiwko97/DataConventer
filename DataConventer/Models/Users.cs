using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration.Attributes;


namespace DataConventer.Models
{
    public class Users:IUsers,IParseFromCsv
    {
        public string EmployeeType { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string EndDate { get; set; }
        public string Extension1 { get; set; }

        public static  async Task<Users> ParseFromCsvAsync(string line)
        {
            
            String[] columns = line.Split(',');


            await Task.Run(() => ValidEmail(columns[4]));
            await Task.Run(() => ValidEmail(columns[4]));
            await Task.Run(() => ValidEmail(columns[4]));


            //ValidEmail(columns[4]);
            //ValidEmail(columns[4]);
            //ValidEmail(columns[4]);


            return new Users
            {
                EmployeeType = columns[0],
                Id = columns[1],
                FirstName = columns[2],
                LastName = columns[3],
                Email = columns[4],
                Gender = columns[5],
                EndDate = columns[6],
                Extension1 = columns[7]
            };
        }

       public static int ValidEmail(string column)
        {
            Console.WriteLine(DateTime.Now.ToString("hh.mm.ss.ffffff"));
            Regex r = new Regex("\"(.+?)\"");
            MatchCollection mc = r.Matches(column);
            try
            {
                var value = mc[0].Groups[1].Value;
                Console.WriteLine(value);
                try
                {
                    var addr = new MailAddress(value);
                    if (addr.Address == value)
                    {
                        Console.WriteLine("Email Adres!");
                    }
                }
                catch
                {
                    Console.WriteLine("");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return 0;
        }
    }
}
