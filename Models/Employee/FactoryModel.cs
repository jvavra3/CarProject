using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarProject.Models.Employee
{
    public class FactoryModel
    {
        [Key]
        public int FactoryId { get; set; }
        [Required]
        [Display(Name = "Factory Name")]
        public string FactoryName { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public int PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Type of Car")]
        public string TypeOfCar { get; set; }
        public ICollection<EmployeeModel> EmployeeModels { get; set; }
       


    }
}