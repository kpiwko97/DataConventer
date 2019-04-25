using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DataConventer.Models
{
    public class Employee:IPhone,IUsers
    {     
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "employee_type")]
        public TypeOfWork EmployeeType { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "login")]
        public string Login { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "gender")]
        public Sex Gender { get; set; }

        [JsonProperty(PropertyName = "end_date")]
        public string EndDate { get; set; }

        [JsonProperty(PropertyName = "ext1")]
        public string Extension1 { get; set; }

        [JsonProperty(PropertyName = "phone1")]
        public string Phone1 { get; set; }

        [JsonProperty(PropertyName = "phone2")]
        public string Phone2 { get; set; }

        [JsonProperty(PropertyName = "phone3")]
        public string Phone3 { get; set; }
    }
}
