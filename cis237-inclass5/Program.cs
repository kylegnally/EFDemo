using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cis237_inclass5
{
    class Program
    {
        static void Main(string[] args)
        {
            // Make new instance of the Cars Collection
            // Probably going to be CarsFInitialLName for you.
            CarsTestEntities carsTestEntities = new CarsTestEntities();

            //***************************************
            // List out all of the cars in the table
            //***************************************
            Console.WriteLine("Print the list");

            foreach (Car car in carsTestEntities.Cars)
            {
                Console.WriteLine(car.id + " " + car.make + " " + car.model);
            }

            //***************************************
            // Find a specifc one by any property
            //***************************************

            // Call the Where method on the table Cars and pass in a lambda expression
            // for the criteria we are looking for. There is nothing special about the
            // word car in the part that reads: car => car.id == "V0...". It could be
            // any characters we want it to be.
            // It is just a variable name for the current car we are considering
            // in the expression. This will automagically loop through all the Cars,
            // and run the expression against each of them. When the result is finally
            // true, it will return that car.
            Car carToFind = carsTestEntities.Cars.Where(
                car => car.id == "V0LCD1814").First();

            // We can look for a specific model from the database with a where based
            // on any criteria we want. Here is one that is looking to match the Car's
            // model instead of the id.
            List<Car> resultList = carsTestEntities.Cars.Where(
                car => car.model == "Challenger").ToList();

            // Print the cars out.
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Find 1 specific car with the Where method");
            Console.WriteLine(carToFind.id + " " + carToFind.make + " " + carToFind.model);
            Console.WriteLine();
            Console.WriteLine("Find any cars that have a model of challenger");
            foreach (Car car in resultList)
            {
                Console.WriteLine(car.id + " " + car.make + " " + car.model);
            }

            //************************************
            // Find a car based on the primary key
            //************************************

            // Pull out a car fro mthe table based on the id which is the primary key
            // If the record doesn't exist in the database, it will return null, so check
            // what you get back and see if it is null. If so, it doesn't exist.
            Car foundCar = carsTestEntities.Cars.Find("V0LCD1814");

            Console.WriteLine();
            Console.WriteLine("Find 1 specific car with the Find method");
            Console.WriteLine(foundCar.id + " " + foundCar.make + " " + foundCar.model);

            //***********************************************
            // Add a new Car to the database
            //***********************************************

            // Make an instance of a new car
            Car newCarToAdd = new Car();

            // Assign properties to the parts of the model
            newCarToAdd.id = "88888";
            newCarToAdd.make = "Nissan";
            newCarToAdd.model = "GT-R";
            newCarToAdd.horsepower = 550;
            newCarToAdd.cylinders = 8;
            newCarToAdd.year = "2018";
            newCarToAdd.type = "Car";

            // Use a try catch to ensure that they can't add a car with an id that
            // already exists
            try
            {
                // Add the new car to the Cars collection
                carsTestEntities.Cars.Add(newCarToAdd);
                // This method call actually does the work of saving the changes to the db.
                carsTestEntities.SaveChanges();
            }
            catch (Exception e)
            {
                // Remove the new car from the Cars Collection since we can't save it
                carsTestEntities.Cars.Remove(newCarToAdd);
                carsTestEntities.SaveChanges();
                Console.WriteLine("Can't add the record. Already have one with that primary key");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Just added a new car. Going to fetch it out and print it to verify");
            carToFind = carsTestEntities.Cars.Find("88888");
            Console.WriteLine(carToFind.id + " " + carToFind.make + " " + carToFind.model);

            //***********************************************
            // Delete a Car from the database
            //***********************************************

            // Get a car out of the database that we want to delete
            Car carToFindForDelete = carsTestEntities.Cars.Find("88888");

            // Remove the Car from the Cars Collection
            carsTestEntities.Cars.Remove(carToFindForDelete);

            // Save the changes to the database
            carsTestEntities.SaveChanges();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Deleted the added car. Looking to see if it is still in the db");

            // Try to get the car out of the database and print it out
            carToFindForDelete = carsTestEntities.Cars.Find("88888");
            if (carToFindForDelete == null)
            {
                Console.WriteLine("The model you are looking for does not exist");
            }
        }
    }
}
