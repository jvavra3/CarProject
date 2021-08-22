using CarProject.Filters;
using CarProject.Models.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarProject.Repository
{
    public class LogRepository: Controller, ILogRepository
    {
        //access data from database
        private FilterContext db = new FilterContext();

        public IEnumerable<LogModel> GetAllLogs()
        {

            return db.Logs.ToList();

        }

      

        public void CreateNewLog(LogModel logToCreate)
        {


            db.Logs.Add(logToCreate);
            db.SaveChanges();

        }

        public void DeleteLog(int id)
        {
            LogModel conToDel = GetLogByID(id);
            db.Logs.Remove(conToDel);
            db.SaveChanges();

        }

        public LogModel GetLogByID(int? id)
        {
            return db.Logs.FirstOrDefault(d => d.LogId == id);
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }
    }
    public interface ILogRepository : IDisposable
    {
        IEnumerable<LogModel> GetAllLogs();
        void CreateNewLog(LogModel logToCreate);
        void DeleteLog(int id);
        LogModel GetLogByID(int? id);
        int SaveChanges();
    }
}