using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarProject.Models.Employee
{
    public class EmployeeModel
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name ="Phone Number")]
        public int PhoneNumber { get; set; }

        //matches with the primary key -> FactoryModel
        //FactoryId become a foreign key
        //unique name of the factory
        [Required]
        [Display(Name = "Factory Name")]
        public string FactoryName { get; set; }
        public FactoryModel Factory { get; set; }

    }
}