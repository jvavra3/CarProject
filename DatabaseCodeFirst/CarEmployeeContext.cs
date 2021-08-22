using CarProject.Models.Employee;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CarProject.DatabaseCodeFirst
{//DbContext ->session with database
    public class CarEmployeeContext:DbContext
    {

        public CarEmployeeContext() : base("CarEmployeeDatabase")
        {
            Database.SetInitializer<CarEmployeeContext>(new DropCreateDatabaseIfModelChanges<CarEmployeeContext>());

        }

        //Database tables(CarEmployee) - preparation for migration
        public DbSet<EmployeeModel> EmployeeModels { get; set; }
        public DbSet<FactoryModel> FactoryModels { get; set; }
   


       


    }
}