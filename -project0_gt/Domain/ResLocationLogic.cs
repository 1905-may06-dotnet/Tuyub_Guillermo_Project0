using System;
using System.Collections.Generic;
using System.Linq;
using Data.Models;

namespace Domain
{

    public class ResLocationLogic
    {
        //DATABASE -> LOCAL STORAGE
        List<ResLocation> locations = Crud.DbInstance.Instance.ResLocation.ToList<ResLocation>();
        string x;
        public int chooseRestaurant()
        {
            Console.WriteLine("Choose Restaurant: ");
            showLocation();
            x = Console.ReadLine();
            if (Int32.TryParse(x, out int number)){

                ResLocation rel = locations[number-1];

                if (rel != null)
                {
                    Console.WriteLine("\t\tChosen: " + rel.ResName);
                    return rel.LocationId;
                }
                else
                {
                    Console.WriteLine("Insert a number 1-3: ");
                    return chooseRestaurant();
                }
            }
            else
            {
                Console.WriteLine("Insert a number 1-3: ");
                return chooseRestaurant();

            }

        }
        public void showLocation()
        {
            foreach (ResLocation res in locations)
                Console.WriteLine($"{res.LocationId}) " + res.ResName);

        }

    }
}
