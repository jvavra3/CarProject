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
    [Authorize(Roles = "Admin")]
    public class FactoryController : Controller
    {
        //private CarEmployeeContext db = new CarEmployeeContext();
        //_repository initialization
        IFactoryRepository _repository;

        //FactoryController constructor
        //FactoryRepository - get data from database
        public FactoryController() : this(new FactoryRepository()) { }
        // interface for operation with database / (mocking)
        public FactoryController(IFactoryRepository repository)
        {
            _repository = repository;
        }

        // GET: Factory
        public ActionResult Index()
        {
            ViewData["ControllerName"] = this.ToString();
            return View("Index", _repository.GetAllFactory());
            //return View(db.FactoryModels.ToList());
        }

        // GET: Factory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FactoryModel factoryModel = _repository.GetFactoryByID(id);

            if (factoryModel == null)
            {
                return HttpNotFound();
            }
            return View("Details", factoryModel);
        }

        // GET: Factory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Factory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Logging]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "FactoryId")] FactoryModel factoryToModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.CreateNewFactory(factoryToModel);
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


        // GET: Factory/Edit/5
        public ActionResult Edit(int? id = 0)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FactoryModel factoryToEdit = _repository.GetFactoryByID(id);
           
            if (factoryToEdit == null)
            {
                return HttpNotFound();
            }
            return View(factoryToEdit);
        }

        // POST: Factory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Logging]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            FactoryModel editFactory = _repository.GetFactoryByID(id);


            try
            {
                if (TryUpdateModel(editFactory))
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
            return View(editFactory);

        }

        // GET: Factory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FactoryModel delFactory = _repository.GetFactoryByID(id);
            if (delFactory == null)
            {
                return HttpNotFound();
            }
            return View(delFactory);
        }

        // POST: Factory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Logging]
        public ActionResult DeleteConfirmed(int id, FormCollection collection)
        {
            try
            {
                _repository.DeleteFactory(id);
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
