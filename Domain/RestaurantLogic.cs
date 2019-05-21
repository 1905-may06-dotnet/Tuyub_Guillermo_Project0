using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Data.Models;
using Domain;

namespace Domain
{
    class RestaurantDatabase
    {

        List<Restaurant> restaurants;

    }

    public class RestaurantLogic
    {
        pizzadb_gtContext dbui = new pizzadb_gtContext(); //figure out singleton implementation later
                                                          //so only 1 obj is created.
        public ResLocation chooseRestaurant(string x)
        {

            if (Int32.TryParse(x, out int number)){
                //Database -> Program
                ResLocation rel = dbui.ResLocation.Where<ResLocation>(u => u.LocationId == number).FirstOrDefault<ResLocation>();

                //DbInstance.Instance.ResLocation.Add();// calling insert Query
                //DbInstance.Instance.SaveChanges();

                return rel;
            }
            else
            {
                Console.WriteLine("Insert a number 1 -6: ");
                return chooseRestaurant(Console.ReadLine());

            }

        }

        public sealed class DbInstance
        {
            private static pizzadb_gtContext instance = null;
            private DbInstance()
            {
            }
            public static pizzadb_gtContext Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new pizzadb_gtContext();
                        return instance;
                    }
                    else
                    {
                        return instance;
                    }
                }
            }

        }


    }
}
