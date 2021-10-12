using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarProject.DatabaseCodeFirst;
using CarProject.Filters;
using CarProject.Models.Employee;
using CarProject.Repository;

namespace CarProject.Controllers.EmployeeControllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class EmployeeController : Controller
    {
        //private CarEmployeeContext db = new CarEmployeeContext();

        //_repository initialization

        IEmployeeRepository _repository;

        //EmployeeController constructor
        //EmployeeRepository - get data from database
        public EmployeeController() : this(new EmployeeRepository()) { }
        // interface for operation with database / (mocking)
        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        // GET: Employee
        public ActionResult Index()
             {
           
            return View("Index", _repository.GetAllEmployee());
            
            //return View(db.EmployeeModels.ToList());

        }
        
        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EmployeeModel employeeModel = _repository.GetEmployeeByID(id);  
            
            //EmployeeModel employeeModel = db.EmployeeModels.Find(id);
            if (employeeModel == null)
            {
                return HttpNotFound();
            }
            //return View(employeeModel);
            return View("Details", employeeModel);  
        }

             
        
        // GET: Employee/Create
        public ActionResult Create()
        {

            ViewBag.FactoryName = new SelectList(_repository.GetAllFactory(), "FactoryName", "FactoryName");
            

            return View();


            
        }

        
        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Logging]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "EmployeeId,FirstName,LastName,Email,PhoneNumber,FactoryName")] EmployeeModel employeeModel)
        public ActionResult Create([Bind(Exclude = "EmployeeId")] EmployeeModel employeeToCreate)

        {
            //ModelState.IsValid - if it was possible to bind the incoming values from the request to the model correctly (form and model)
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.CreateNewEmployee(employeeToCreate);
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

        

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id = 0)
        {
            
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //EmployeeModel employeeModel = db.EmployeeModels.Find(id);
            EmployeeModel employeeToEdit = _repository.GetEmployeeByID(id);

            if (employeeToEdit == null)
            {
                return HttpNotFound();
            }
            return View(employeeToEdit);
        }



        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //FormCollection - submitted form with information
        [Logging]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            EmployeeModel editEmployee = _repository.GetEmployeeByID(id);


            try
            {
                if (TryUpdateModel(editEmployee))
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
            return View(editEmployee);
            /*
            if (ModelState.IsValid)
            {
                db.Entry(employeeModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeModel);
            */
        }

       

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeModel delEmployee = _repository.GetEmployeeByID(id);

            //EmployeeModel employeeModel = db.EmployeeModels.Find(id);

            if (delEmployee == null)
            {
                return HttpNotFound();
            }
            return View(delEmployee);
        }
        

       // POST: Employee/Delete/5
       [HttpPost, ActionName("Delete")]
       [ValidateAntiForgeryToken]
       [Logging]
       public ActionResult DeleteConfirmed(int id, FormCollection collection)
       {
            try
            {
                _repository.DeleteEmployee(id);
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
