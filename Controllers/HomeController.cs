using CarProject.DatabaseFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarProject.Controllers
{
    public class HomeController : Controller
    {

        private CarDatabaseEntities db = new CarDatabaseEntities();
        public ActionResult DashBoard()
        {


            return View();

        }

        public ActionResult Index()
        {
            var list = db.Car.ToList();
            List<int> annualSalaryMale = new List<int>();
            List<int> annualSalaryFemale = new List<int>();
            List<int> debtMale = new List<int>();
            List<int> debtFemale = new List<int>();
            List<int> purchaseMale = new List<int>();
            List<int> purchaseFemale = new List<int>();

            List<int> ageFull = new List<int>();
            List<int> annualSalary = new List<int>();
            List<int> purchase = new List<int>();


            List<int> numberOfGenders = new List<int>();
            List<int> uniqueAge = new List<int>();
            var gender = list.Select(x => x.Gender).Distinct();
            var age = list.Select(x => x.Age).Distinct().OrderBy(Age => Age);
            foreach (var item in age)
            {
                uniqueAge.Add(list.Count(x => x.Age == item));

            }

            foreach (var item in gender)
            {
                numberOfGenders.Add(list.Count(x => x.Gender == item));
                

            }
            annualSalaryMale = list.Where(x => x.Gender == "Male").Select(x => x.Annual_Salary).ToList();
            annualSalaryFemale = list.Where(x => x.Gender == "Female").Select(x => x.Annual_Salary).ToList();

            debtMale = list.Where(x => x.Gender == "Male").Select(x => x.Credit_Card_Debt).ToList();
            debtFemale = list.Where(x => x.Gender == "Female").Select(x => x.Credit_Card_Debt).ToList();

            purchaseMale = list.Where(x => x.Gender == "Male").Select(x => x.Car_Purchase_Amount).ToList();
            purchaseFemale = list.Where(x => x.Gender == "Female").Select(x => x.Car_Purchase_Amount).ToList();

            ageFull = list.Select(x => x.Age).ToList();
            annualSalary = list.Select(x => x.Annual_Salary).ToList();
            purchase = list.Select(x => x.Car_Purchase_Amount).ToList();



            ViewBag.gender = gender.ToList();
            ViewBag.numberOfGenders = numberOfGenders.ToList();

            ViewBag.age = age.ToList();
            ViewBag.uniqueAge = uniqueAge.ToList();

            ViewBag.annualSalaryMale = annualSalaryMale;
            ViewBag.annualSalaryFemale = annualSalaryFemale;

            ViewBag.debtMale = debtMale;
            ViewBag.debtFemale = debtFemale;

            ViewBag.purchaseMale = purchaseMale;
            ViewBag.purchaseFemale = purchaseFemale;

            ViewBag.ageFull = ageFull;
            ViewBag.annualSalary = annualSalary;
            ViewBag.purchase = purchase;
         


            return View();

            
        }
        /*
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        */

    }
}