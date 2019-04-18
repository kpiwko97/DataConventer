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
        public TypeOfWork EmployeeType { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Sex Gender { get; set; }
        public string EndDate { get; set; }
        public string Extension1 { get; set; }

        public enum TypeOfWork
        {
            WrongType,
            Contractor,
            FullTime,
        }
        public enum Sex
        {
            Male,
            Female
        }

        public static async Task<Users> ParseFromCsvAsync(string line)
        {           
            String[] columns = line.Replace("\"", "").Split(',');
            Console.WriteLine(DateTime.Now.ToString("hh.mm.ss.fff"));//Performance
            return new Users
            {
                EmployeeType = await Task.Run(() => ValidPropertyEnum<TypeOfWork>(columns[0])),
                Id = await Task.Run(() => ValidId(columns[1])),
                FirstName = columns[2],
                LastName = columns[3],
                Email = await Task.Run(() => ValidEmail(columns[4])),
                Gender = await Task.Run(() => ValidPropertyEnum<Sex>(columns[5])),
                EndDate = await Task.Run(() => ValidEndDate(columns[6])),
                Extension1 = columns[7]
            };
        }

        public static TEnum ValidPropertyEnum<TEnum>(string column) where TEnum : struct
       {
            Enum.TryParse(column, out TEnum enumType);
            return enumType;
       }
        public static string ValidId(string column)
       {
            if (String.IsNullOrWhiteSpace(column)) Console.WriteLine("Empty");
            return column;
       }
        public static string ValidEmail(string column)
       {
            try
            {
                new MailAddress(column);
                return column;
            }
            catch
            {
                Console.WriteLine("Inncorect Email Adress!");
                return String.Empty;
            }
       }
        public static string ValidEndDate(string column)
        {
            DateTime.TryParse(column,out DateTime result);
            return result>DateTime.Now? result.ToShortDateString():string.Empty;
        }
    }
}
