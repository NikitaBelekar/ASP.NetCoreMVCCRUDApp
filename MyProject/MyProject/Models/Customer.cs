using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace MyProject.Models
{
    public class Customer
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

       
        public string salutation { get; set; }
        public string initials { get; set; }

        [Display(Name = "First Name")]
        public string firstname { get; set; }

        public string firstname_ascii { get; set; }

        [Display(Name = "Gender")]
        public string gender { get; set; }

        public int firstname_country_rank { get; set; }
        public int firstname_country_frequency { get; set; }

        [Display(Name = "Last Name")]
        public string lastname { get; set; }

        public string lastname_ascii { get; set; }
        public string lastname_country_rank { get; set; }
        public string lastname_country_frequency { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter Email ID")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string email { get; set; }
        public string password { get; set; }

        [Display(Name = "Country Code")]
        public string country_code { get; set; }
        public string country_code_alpha { get; set; }

        public string country_name { get; set; }
        public string primary_language_code { get; set; }
        public string primary_language { get; set; }


        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Display(Name = "Balance")]
        public double balance { get; set; }

        [Display(Name = "Phone Number")]
        public string phone_Number { get; set; }


        public string currency { get; set; }
        public string partitionKey { get; set; }
        public int rowKey { get; set; }

        public DateTime timestamp { get; set; }

        // public int eTag { get; set; }
        public string[] Include { get; }


   



    }
}
