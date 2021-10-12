using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarProject.Models
{
    public class RoleAssigment
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public bool isChecked { get; set; }


    }

    
    public class UserRoleViewModel
    {
        public UserRoleViewModel ()
            {
            this.UserRoles = new List<RoleAssigment>();
            }
        public string userID { get; set; }
        public string userName { get; set; }

        public List<RoleAssigment> UserRoles { get; set; }


    }
}