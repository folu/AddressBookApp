using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AddressBookApp.Models
{
    public class Person
    {
       
        public int Id { get; set; }
        [Display(Name = "FirstName")]
        public string first_name { get; set; }
        [Display(Name = "LastName")]
        public string last_name { get; set; }
        [StringLength(11)]  //should always be 11 max for both mobile and land line
        public string phone { get; set; } 

        public string email { get; set; }
    }
}
