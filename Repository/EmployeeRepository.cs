using CarProject.DatabaseCodeFirst;
using CarProject.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace CarProject.Repository
{
    public class EmployeeRepository: Controller, IEmployeeRepository
    {
        //access data from database
        private CarEmployeeContext db = new CarEmployeeContext();
       
        public IEnumerable<EmployeeModel> GetAllEmployee()
        {
            
            return db.EmployeeModels.ToList();

        }

        public IEnumerable<FactoryModel> GetAllFactory()
        {

            return db.FactoryModels.ToList();

        }

        public void CreateNewEmployee(EmployeeModel employeeToCreate)
        {
            
            
            db.EmployeeModels.Add(employeeToCreate);
            db.SaveChanges();

        }

        public void DeleteEmployee(int id)
        {
            EmployeeModel conToDel = GetEmployeeByID(id);
            db.EmployeeModels.Remove(conToDel);
            db.SaveChanges();

        }

        public EmployeeModel GetEmployeeByID(int? id)
        {
            return db.EmployeeModels.FirstOrDefault(d => d.EmployeeId == id);
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }
    }
    public interface IEmployeeRepository: IDisposable
    {
        IEnumerable<FactoryModel> GetAllFactory();
        IEnumerable<EmployeeModel> GetAllEmployee();
        void CreateNewEmployee(EmployeeModel employeeToCreate);
        void DeleteEmployee(int id);
        EmployeeModel GetEmployeeByID(int? id);
        int SaveChanges();
    }
}