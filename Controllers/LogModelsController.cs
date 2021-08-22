using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarProject.Filters;
using CarProject.Models.Logs;
using CarProject.Repository;

namespace CarProject.Controllers
{
    public class LogModelsController : Controller
    {
        


        //_repository initialization
        ILogRepository _repository;
      

        //LogRepository - get data from database
        public LogModelsController() : this(new LogRepository()) { }
        // interface for operation with database / (mocking)

        public LogModelsController(ILogRepository repository)
        {
            _repository = repository;
        }

        // GET: LogModels
        public ActionResult Index()
        {
            ViewData["ControllerName"] = this.ToString();
            return View("Index", _repository.GetAllLogs());
        }

        // GET: LogModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogModel logModel = _repository.GetLogByID(id);
            
            if (logModel == null)
            {
                return HttpNotFound();
            }
            return View("Details", logModel);
        }

        // GET: LogModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LogModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "LogId")] LogModel logModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _repository.CreateNewLog(logModel);
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

        // GET: LogModels/Edit/5
        public ActionResult Edit(int? id = 0)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            LogModel logToEdit = _repository.GetLogByID(id);
            
            if (logToEdit == null)
            {
                return HttpNotFound();
            }
            return View(logToEdit);
        }

        // POST: LogModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            LogModel editLog = _repository.GetLogByID(id);


            try
            {
                if (TryUpdateModel(editLog))
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
            return View(editLog);
        }

        // GET: LogModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogModel delLog = _repository.GetLogByID(id);
            if (delLog == null)
            {
                return HttpNotFound();
            }
            return View(delLog);
        }

        // POST: LogModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, FormCollection collection)
        {
            try
            {
                _repository.DeleteLog(id);
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
