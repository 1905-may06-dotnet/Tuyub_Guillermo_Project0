using System;
using System.Collections.Generic;
using System.Linq;
using Data.Models;

namespace Domain
{

    public class RestaurantLogic
    {
        List<ResLocation> locations = Crud.DbInstance.Instance.ResLocation.ToList<ResLocation>();

        public ResLocation chooseRestaurant(string x)
        {

            if (Int32.TryParse(x, out int number)){
                //Database -> Program
                ResLocation rel = locations[number-1];

                if (rel != null)
                    return rel;
                else
                {
                    Console.WriteLine("Insert a number 1-3: ");
                    return chooseRestaurant(Console.ReadLine());
                }
            }
            else
            {
                Console.WriteLine("Insert a number 1-3: ");
                return chooseRestaurant(Console.ReadLine());

            }

        }
        public void showLocation()
        {
            foreach (ResLocation res in locations)
                Console.WriteLine($"{res.LocationId}) " + res.ResName);

        }

    }
}
