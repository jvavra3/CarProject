using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace CarProject.Methods
{
    public class Collections:Controller
    {


        public static List<string> CollectionOfCountries()
        {

            //list of countries -> create and edit CarDatabase Views
            List<string> CountryList = new List<string>();
            CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo CInfo in CInfoList)
            {
                RegionInfo R = new RegionInfo(CInfo.LCID);
                if (!(CountryList.Contains(R.EnglishName)))
                {
                    CountryList.Add(R.EnglishName);
                }
            }

            CountryList.Sort();
          
            return (CountryList);


        }


        public static List<string> CollectionOfGenders()
        {
            //list of Genders -> create and edit CarDatabase Views
            var Genders = new List<string>();
            Genders.Add("Male");
            Genders.Add("Female");

       



            return(Genders);


        }
        public static List<string> CarTypes()
        {
            //list of Genders -> create and edit CarDatabase Views
            var Cars = new List<string>();
            Cars.Add("Coupe");
            Cars.Add("Sport Car");
            Cars.Add("Hatchback");
            Cars.Add("Station Wagon");
            Cars.Add("Minivan");
            Cars.Add("Pickup Truck");
            return (Cars);


        }
        /*
        public List<string> FactoryName ()
        {
            string connection = ConfigurationManager.ConnectionStrings["CarEmployeeDatabase"].ConnectionString;
            SqlConnection sqlcon = new SqlConnection(connection);
            string sqlquery = "select * from [dbo].[FactoryModels]";
            SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon);
            sqlcon.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcom);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            ViewBag

            List<SelectListItem> getFactoryName = new List<SelectListItem>;


            sqlcon.Close();
            return ();

        }
        */

        //ViewBag.FactoryName = new SelectList(db.FactoryModels, "Factory Name", "FactoryName");


    }

    


    
}