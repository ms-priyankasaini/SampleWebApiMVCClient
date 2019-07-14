using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SampleWebApiClient.Models
{
    public class Employee
    {
        [Required(ErrorMessage = "Employee id is required")]
        [Display(Name="Emp ID")]
        public int id { get; set; }  //Numeric only and unique..

        [Display(Name = "Designation")]
        [Required(ErrorMessage = "Job Title is required")]
        public string job_title { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string first_name { get; set; }

        [Display(Name = "Last Name")]
        public string last_name { get; set; }

        [Display(Name = "Department")]
        [Required(ErrorMessage = "Department is Required")]
        public string department { get; set; }

        [Display(Name = "Phone No.")]
        public string phone { get; set; } // only numeric and - permitted

        [Display(Name = "Email ID")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string email { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country is Required")]
        public string country { get; set; }

        [Display(Name = "State")]
        public string state { get; set; }

        [Display(Name = "City")]
        public string city { get; set; }
    }
}