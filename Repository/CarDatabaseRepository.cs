using CarProject.DatabaseFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarProject.Repository
{
    public class CarDatabaseRepository: Controller, ICarRepository
    {
        private CarDatabaseEntities db = new CarDatabaseEntities();

            
        

        public IEnumerable<Car> GetAllCars(string Name, string Gender, string Country, string Email, string Age, string Salary, string Debt, string netWorth, string Purchase, string Sort)
        {

            


         
            //LINQ Query
            var carData = from c in db.Car
                          select c;

            //search result (data) by words
            if (!string.IsNullOrEmpty(Name))
            {

                carData = carData.Where(c => c.Customer_Name.Contains(Name));
            }
            if (!string.IsNullOrEmpty(Email))
            {

                carData = carData.Where(c => c.Customer_email.Contains(Email));
            }
            if (!string.IsNullOrEmpty(Gender))
            {

                carData = carData.Where(c => c.Gender == Gender);
            }
            if (!string.IsNullOrEmpty(Country))
            {

                carData = carData.Where(c => c.Country.Contains(Country));
            }
            if (!string.IsNullOrEmpty(Age))

            {

                carData = carData.Where(c => c.Age.ToString() == Age);
            }

            if (!string.IsNullOrEmpty(Salary))

            {

                carData = carData.Where(c => c.Annual_Salary.ToString() == Salary);
            }
            if (!string.IsNullOrEmpty(Debt))

            {

                carData = carData.Where(c => c.Credit_Card_Debt.ToString() == Debt);
            }
            if (!string.IsNullOrEmpty(netWorth))

            {

                carData = carData.Where(c => c.Net_Worth.ToString() == netWorth);
            }
            if (!string.IsNullOrEmpty(Purchase))

            {

                carData = carData.Where(c => c.Car_Purchase_Amount.ToString() == Purchase);
            }


            //return db.Car.ToList();
            return carData.ToList();

        }
        public void CreateNewCar(Car carToCreate)
        {


            db.Car.Add(carToCreate);
            db.SaveChanges();

        }

        public void DeleteCar(int id)
        {
            Car carToDel = GetCarByID(id);
            db.Car.Remove(carToDel);
            db.SaveChanges();

        }

        public Car GetCarByID(int? id)
        {
            return db.Car.FirstOrDefault(d => d.CarId == id);
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }

       

    }


    public interface ICarRepository: IDisposable
    {
       
        IEnumerable<Car> GetAllCars(string Name, string Gender, string Country, string Email, string Age, string Salary, string Debt, string netWorth, string Purchase, string Sort);
        void CreateNewCar(Car carToCreate);
        void DeleteCar(int id);
        Car GetCarByID(int? id);
        int SaveChanges();


    }
}