using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CarProject.Models;

namespace CarProject.Controllers
{  
    [Authorize(Roles = "Admin")]
    
    public class RolesAdminController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        // GET: RolesAdmin
        public ActionResult Index()
        {
            var roles = context.Roles.ToList();
            return View(roles);
        }
        public ActionResult ListOfUsers()
        {
            var users = context.Users.OrderBy(u => u.UserName).ToList();


            return View(users);
        }
    
        //GET:
        public ActionResult Create()
        {
            return View();
        }
        

        //POST RoleAdmin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            
            // IdentityResult - error occurance during identity operations
            IdentityResult identityResult = new IdentityResult();
            //RoleManager - API to manage roles
            //IdentityRole - class that contains information about user roles (which are usage domains) of the IdentityUsers defined in application.
            //RoleStore - takes a database context and wires up the stores with default instances using the context.
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            identityResult = roleManager.Create(new IdentityRole(collection["RoleName"]));

            
            if (identityResult.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = identityResult.Errors.ToString();
                return View();

            }


        }

        // Delete role
        public ActionResult DeleteRoles(string roleName)
        {
            
            // IdentityResult - error occurance during identity operations
            IdentityResult identityResult = new IdentityResult();
            //RoleManager - API to manage roles
            //IdentityRole - class that contains information about user roles (which are usage domains) of the IdentityUsers defined in application.
            //RoleStore - takes a database context and wires up the stores with default instances using the context.
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var deletedRole = roleManager.FindByName(roleName);


            identityResult = roleManager.Delete(deletedRole); ;


            if (identityResult.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = identityResult.Errors.ToString();
                return View();

            }
          


        }
        // Edit name of role - get
        // GET: /Roles/Edit/5
        public ActionResult EditRole(string roleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(thisRole);
        }

        // Edit name of role - post
        // POST: /Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(IdentityRole role)
        {
            try
            {
                context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        // Listed users and theirs roles
        [HttpPost]
        public ActionResult ManageUserRoles(FormCollection form)
        {
            UserRoleViewModel urvModel = new UserRoleViewModel();
            //take username from index
            var userName = form["UserName"];

            //take all credentials from context (id etc.) - one user at the time.
            var user = context.Users.Where(u => u.UserName == userName.Trim()).FirstOrDefault();

            if (user != null)

            {
                var roles = context.Roles.ToList();
                urvModel.userName = userName;
                urvModel.userID = user.Id;

                foreach (var item in roles)
                {
                    RoleAssigment roleAssigment = new RoleAssigment();
                    roleAssigment.Name = item.Name;
                    roleAssigment.Id = item.Id;
                    roleAssigment.isChecked = false;

                    if (user.Roles != null)
                    {
                        //select all roles according to RoleId. (list of roles)
                        var roleIDs = user.Roles.Select(r => r.RoleId).ToList();
                        //chcecked for roleIDs with roles
                        if (roleIDs.Contains(item.Id))
                        {
                            roleAssigment.isChecked = true;
                        }
                        else
                        {
                            roleAssigment.isChecked = false;
                        }

                    }
                    urvModel.UserRoles.Add(roleAssigment);



                }


            }
            else
            {
                urvModel = null;
            }
            return View(urvModel);

        }

        // Update roles for one user
        [HttpPost]
        public ActionResult UpdateRoles(UserRoleViewModel updateRoles)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var userID = updateRoles.userID;

            foreach (var item in updateRoles.UserRoles)
            {
                if (item.isChecked)
                {
                    if (!userManager.IsInRole(userID,item.Id))
                    {
                        userManager.AddToRole(userID, item.Name);

                    }

                }
                else if (userManager.IsInRole(userID,item.Name))
                {
                    userManager.RemoveFromRoles(userID,item.Name);

                }

            }
            return RedirectToAction("Index");
            
        }
    } 
}
