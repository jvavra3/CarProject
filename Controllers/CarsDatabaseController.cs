using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarProject.DatabaseFirst;
using CarProject.Filters;
using CarProject.Methods;
using CarProject.Repository;
//Template controler (Entity Framework)
namespace CarProject.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class CarsDatabaseController : Controller
    {
        //private CarDatabaseEntities db = new CarDatabaseEntities();


        //_repository initialization
        ICarRepository _repository;
        //carData initialization
        private IEnumerable<Car> carData;

        //CarsDatabaseController constructor
        //CarDatabaseRepository - get data from database
        public CarsDatabaseController() : this(new CarDatabaseRepository()) { }
        // interface for operation with database / (mocking)

        public CarsDatabaseController(ICarRepository repository)
        {
            _repository = repository;
        }
        //private CarDatabaseEntities db = new CarDatabaseEntities();

        // GET: CarsDatabase

        public ActionResult Index(string Name, string Gender, string Country, string Email, string Age, string Salary, string Debt, string netWorth, string Purchase, string Sort)
        {
            
            

            carData = _repository.GetAllCars(Name, Gender, Country, Email, Age, Salary, Debt, netWorth, Purchase, Sort);

            ViewBag.nameSort = Sort == "CustomerAsc" ? "CustomerDesc" : "CustomerAsc";
            ViewBag.emailSort = Sort == "EmailAsc" ? "EmailDesc" : "EmailAsc";
            ViewBag.genderSort = Sort == "GenderAsc" ? "GenderDesc" : "GenderAsc";
            ViewBag.countrySort = Sort == "CountryAsc" ? "CountryDesc" : "CountryAsc";
            ViewBag.ageSort = Sort == "AgeAsc" ? "AgeDesc" : "AgeAsc";
            ViewBag.salarySort = Sort == "SalaryAsc" ? "SalaryDesc" : "SalaryAsc";
            ViewBag.debtSort = Sort == "DebtAsc" ? "DebtDesc" : "DebtAsc";
            ViewBag.worthSort = Sort == "WorthAsc" ? "WorthDesc" : "WorthAsc";
            ViewBag.carPurchaseSort = Sort == "CarPurchaseAsc" ? "CarPurchaseDesc" : "CarPurchaseAsc";

            
            
            ViewData["ControllerName"] = this.ToString();
            //Sorting
            switch (Sort)
            {
                case "CustomerDesc":
                    carData = carData.OrderByDescending(c => c.Customer_Name);
                    break;
                case "CustomerAsc":
                    carData = carData.OrderBy(c => c.Customer_Name);
                    break;
                case "GenderDesc":
                    carData = carData.OrderByDescending(c => c.Gender);
                    break;
                case "GenderAsc":
                    carData = carData.OrderBy(c => c.Gender);
                    break;
                case "EmailDesc":
                    carData = carData.OrderByDescending(c => c.Customer_email);
                    break;
                case "EmailAsc":
                    carData = carData.OrderBy(c => c.Customer_email);
                    break;
                case "CountryDesc":
                    carData = carData.OrderByDescending(c => c.Country);
                    break;
                case "CountryAsc":
                    carData = carData.OrderBy(c => c.Country);
                    break;
                case "AgeDesc":
                    carData = carData.OrderByDescending(c => c.Age);
                    break;
                case "AgeAsc":
                    carData = carData.OrderBy(c => c.Age);
                    break;
                case "SalaryDesc":
                    carData = carData.OrderByDescending(c => c.Annual_Salary);
                    break;
                case "SalaryAsc":
                    carData = carData.OrderBy(c => c.Annual_Salary);
                    break;
                case "DebtDesc":
                    carData = carData.OrderByDescending(c => c.Credit_Card_Debt);
                    break;
                case "DebtAsc":
                    carData = carData.OrderBy(c => c.Credit_Card_Debt);
                    break;
                case "WorthDesc":
                    carData = carData.OrderByDescending(c => c.Net_Worth);
                    break;
                case "WorthAsc":
                    carData = carData.OrderBy(c => c.Net_Worth);
                    break;
                case "CarPurchaseDesc":
                    carData = carData.OrderByDescending(c => c.Car_Purchase_Amount);
                    break;
                case "CarPurchaseAsc":
                    carData = carData.OrderBy(c => c.Car_Purchase_Amount);
                    break;
                default:
                    carData = carData.OrderBy(c => c.Customer_Name);
                    break;


            }


            return View("Index", carData);
        }

        // GET: CarsDatabase/Details/5
        public ActionResult Details(int? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car carModel = _repository.GetCarByID(id);

            if (carModel == null)
            {
                return HttpNotFound();
            }
            return View("Details", carModel);
        }

        // GET: CarsDatabase/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {

            //collectionOfStrings();
            Collections.CollectionOfCountries().ForEach(Console.WriteLine);

            return View();
        }



        // POST: CarsDatabase/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Logging]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Exclude = "CarId")] Car car)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _repository.CreateNewCar(car);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                ViewData["CreateError"] = "Unable to create; view innerexception";
            }

            return View("Create");

        }

        // GET: CarsDatabase/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = _repository.GetCarByID(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            //collectionOfStrings();
            return View(car);
        }

        // POST: CarsDatabase/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Logging]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, FormCollection collection)
        {

            Car editCar = _repository.GetCarByID(id);


            try
            {
                if (TryUpdateModel(editCar))
                {
                    _repository.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception exp)
            {
                if (exp.InnerException != null)
                    ViewData["EditError"] = exp.InnerException.ToString();
                else
                    ViewData["EditError"] = exp.ToString();
            }
            return View(editCar);


          
        }

        // GET: CarsDatabase/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car delCar = _repository.GetCarByID(id);
            if (delCar == null)
            {
                return HttpNotFound();
            }
            return View(delCar);
        }

        // POST: CarsDatabase/Delete/5
        [Logging]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            
            try
            {
                _repository.DeleteCar(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
