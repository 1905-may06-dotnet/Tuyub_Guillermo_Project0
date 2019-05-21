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

        pizzadb_gtContext ctx = new pizzadb_gtContext();
        //ing0-ing4, crustFID, sizeFID, Count, OrderFID, cost

        IndivPizza ipz = new IndivPizza();
        Ingredients ing = new Ingredients();

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

        public Crust getCrust(int number)
        {
            Crust ct = ctx.Crust.Where<Crust>(u => u.CrustId == number).FirstOrDefault<Crust>();
            return ct;
        }


        internal List<string> crust = new List<string>();

        public void setCrust(string input)
        {
            crust.Add(input);
        }
        #region setCrust overloads

        public void setCrust(string input, string input2)
        {
            crust.Add(input);
            crust.Add(input2);
        }
        public void setCrust(string input, string input2, string input3)
        {
            crust.Add(input);
            crust.Add(input2);
            crust.Add(input3);
        }
        #endregion
        public void showCrust()
        {
            foreach (string crust in crust) //shows existing toppings and removes chosen topping.
            {
                Console.WriteLine(" " + crust);
            }
        }


        internal List<string> toppings = new List<string>();
        public void showTop()
        {
            foreach (string topping in toppings) //shows existing toppings and removes chosen topping.
            {
                Console.WriteLine(" " + topping);
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


        internal List<string> size = new List<string>();
        public Size getSize(int number)
        {
            Size sz = ctx.Size.Where<Size>(u => u.SizeId == number).FirstOrDefault<Size>();
            return sz;
        }
        public void setSize(string input)
        {
            size.Add(input);
        }
        #region setSize overloads

        public void setSize(string input, string input2)
        {
            size.Add(input);
            size.Add(input2);
        }
        public void setSize(string input, string input2, string input3)
        {
            size.Add(input);
            size.Add(input2);
            size.Add(input3);
        }
        #endregion
        public void showSize()
        {
            foreach (string size in size) //shows existing toppings and removes chosen topping.
            {
                Console.WriteLine(" " + size);
            }
        }

        public void setCost(decimal? price)
        {
            ipz.Totalcost = price;
        }

        public Ingredients obtainCost(string name)
        {
            Ingredients x = ctx.Ingredients.Where<Ingredients>(u => u.Ingredient == name).FirstOrDefault<Ingredients>();
            if (ipz == null)
            {
                //Count--;
                Console.WriteLine("Fail to obtain Cost, re-enter topping name");
                obtainCost(Console.ReadLine().ToLower());
                return null;
            }
            else if (x.Ingredient == name)
            {
                return x;
            }
            else
                return null;
        }
    }
    //What is contained in pizza, get/set

    public class PizzaOrders
    {
        protected List<PizzaLogic> orders;

        public void addPizzaBuild(PizzaLogic pizza) //add pizza
        {
            orders.Add(pizza);
        }
        public void removePizzaBuild() //remove most recent pizza
        {
            orders.RemoveAt(0);
        }

        public void sendPizzaBuild()
        {
            //code to send pizza info/update customer/update inventory
        }
    } //list of PizzaBuild
}
