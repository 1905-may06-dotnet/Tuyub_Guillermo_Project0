using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{

    public class PizzaLogic
    {
        string ing0, ing1, ing2, ing3, ing4;
        int crustFID;
        int sizeFID;
        int Count;
        int OrderFID;
        decimal cost;

        //ing0-ing4, crustFID, sizeFID, Count, OrderFID, cost
        pizzadb_gtContext ctx = new pizzadb_gtContext();


        IndivPizza ipz = new IndivPizza();
        Ingredients ing = new Ingredients();


        internal List<string> toppings = new List<string>();
        public void setIngredient(Ingredients x,int num)
        {
            if (num == 1)
                ipz.Ingredient0Fid = x.IngId;
            else if (num == 2)
                ipz.Ingredient1Fid = x.IngId;
            else if (num == 3)
                ipz.Ingredient2Fid = x.IngId;
            else if (num == 4)
                ipz.Ingredient3Fid = x.IngId;
            else if (num == 5)
                ipz.Ingredient4Fid = x.IngId;
            else
            {
                Console.WriteLine("default: too many toppings");
                Environment.Exit(0);
            }
        }
        public Ingredients getIngredients(int number)
        {
            Ingredients ing = ctx.Ingredients.Where<Ingredients>(u => u.IngId == number).FirstOrDefault<Ingredients>();
            return ing;
        }
        public void showTop()
        {
            foreach (string topping in toppings) //shows existing toppings and removes chosen topping.
            {
                Console.WriteLine(")" + topping);
            }
        }
        public void setTop(string input)
        {
            toppings.Add(input);
        }
        public void rmTop(string input)
        {
            toppings.Remove(input);
        }
        #region setTop overloads

        public void setTop(string input, string input2)
        {
            toppings.Add(input);
            toppings.Add(input2);
        }
        public void setTop(string input, string input2, string input3)
        {
            toppings.Add(input);
            toppings.Add(input2);
            toppings.Add(input3);
        }

        #endregion
        public Ingredients obtainTopObj(string name)
        {
            Ingredients x = ctx.Ingredients.Where<Ingredients>(u => u.Ingredient == name).FirstOrDefault<Ingredients>();
            if (x == null)
            {
                //Count--;
                Console.WriteLine("Fail to obtain Cost, re-enter topping name");
                obtainTopObj(Console.ReadLine().ToLower());
                return null;
            }
            else if (x.Ingredient == name)
            {
                return x;
            }
            else
                return null;
        }



        internal List<string> crust = new List<string>();
        public Crust getCrust(int number)
        {
            Crust ct = ctx.Crust.Where<Crust>(u => u.CrustId == number).FirstOrDefault<Crust>();
            return ct;
        }
        public Crust getCrust(string name)
        {
            Crust cr = ctx.Crust.Where<Crust>(u => u.Crust1 == name).FirstOrDefault<Crust>();
            if (cr == null)
            {
                //Count--;
                Console.WriteLine("Fail to obtain Cost, re-enter crust name");
                getCrust(Console.ReadLine().ToLower());
                return null;
            }
            else if (cr.Crust1== name)
            {
                return cr;
            }
            else
                return null;
        }
        public void setCrust(string input)
        {
            crust.Add(input);
        }
        public void showCrust()
        {
            foreach (string crust in crust) //shows existing toppings and removes chosen topping.
            {
                Console.WriteLine(")" + crust);
            }
        }




        internal List<string> size = new List<string>();
        public Size getSize(int number)
        {
            Size sz = ctx.Size.Where<Size>(u => u.SizeId == number).FirstOrDefault<Size>();
            return sz;
        }
        public Size getSize(string name)
        {
            Size sz = ctx.Size.Where<Size>(u => u.Size1 == name).FirstOrDefault<Size>();
            if (sz == null)
            {
                //Count--;
                Console.WriteLine("Fail to obtain Cost, re-enter size name");
                getSize(Console.ReadLine().ToLower());
                return null;
            }
            else if (sz.Size1 == name)
            {
                return sz ;
            }
            else
                return null;
        }
        public void setSize(string input)
        {
            size.Add(input);
        }
        public void showSize()
        {
            foreach (string size in size) //shows existing toppings and removes chosen topping.
            {
                
                Console.WriteLine(")" + size + "| price: ");
            }
        }



        public void setCost(decimal? price)
        {
            ipz.Totalcost = price;
        }
        public void showCost()
        {
            Console.WriteLine(ipz.Totalcost);
        }




//--------------------------------------------------------------------Input pizza orders
        public List<IndivPizza> orders;
        public IndivPizza passIndivPizza()
        {
            return ipz;
        }
        public void addPizzaBuild(IndivPizza pizza) //add pizza
        {
            orders.Add(pizza);
        }
        public void removePizzaBuild() //remove most recent pizza
        {
            orders.RemoveAt(0);
        }
        public void rmallPizzaBuild()
        {
            orders.Clear();
        }
        public IndivPizza fromPizzaBuild(int numOfOrder)
        {
            return orders[numOfOrder];
        }

        public void sendPizzaBuild()
        {
            //needs logic to send all pizza's saved in order not just one.
            RestaurantLogic.DbInstance.Instance.IndivPizza.Add(fromPizzaBuild(0));
            RestaurantLogic.DbInstance.Instance.SaveChanges();
        }
    }

}
