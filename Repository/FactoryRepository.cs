using CarProject.DatabaseCodeFirst;
using CarProject.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarProject.Repository
{
    public class FactoryRepository : Controller, IFactoryRepository
    {
        //access data from database
        private CarEmployeeContext db = new CarEmployeeContext();



        public IEnumerable<FactoryModel> GetAllFactory()
        {

            return db.FactoryModels.ToList();

        }

        public void CreateNewFactory(FactoryModel factoryToCreate)
        {


            db.FactoryModels.Add(factoryToCreate);
            db.SaveChanges();

        }

        public void DeleteFactory(int id)
        {
            FactoryModel conToDel = GetFactoryByID(id);
            db.FactoryModels.Remove(conToDel);
            db.SaveChanges();

        }

        public FactoryModel GetFactoryByID(int? id)
        {
            return db.FactoryModels.FirstOrDefault(d => d.FactoryId == id);
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }
    }

    public interface IFactoryRepository: IDisposable
    {
        IEnumerable<FactoryModel> GetAllFactory();
        void CreateNewFactory(FactoryModel factoryToCreate);
        void DeleteFactory(int id);
        FactoryModel GetFactoryByID(int? id);
        int SaveChanges();
    }
}