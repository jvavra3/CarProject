using CarProject.Models.Logs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CarProject.Filters
{
    public class FilterContext:DbContext
    {
        public FilterContext(): base("LogDatabase")
        {
            Database.SetInitializer<FilterContext>(new DropCreateDatabaseIfModelChanges<FilterContext>());


        }

        public DbSet<LogModel> Logs { get; set; }
    }
}