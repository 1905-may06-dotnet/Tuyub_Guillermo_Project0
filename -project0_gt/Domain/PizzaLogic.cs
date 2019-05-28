using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    //Update Required: Split functionality to multiple classes for readability/reusability
    public class PizzaLogic
    {
        string ing0="none", ing1="none", ing2="none", ing3="none", ing4="none";
        string username;
        int crustFID;
        int sizeFID;
        int Count;
        int OrderFID;
        int locationFID;
        int ingFID;
        int userFID;
        decimal cost;

        internal List<Inventory> inventories = Crud.DbInstance.Instance.Inventory.ToList<Inventory>();
        internal List<Ingredients> toppings = Crud.DbInstance.Instance.Ingredients.ToList<Ingredients>();
        internal List<Size> size = Crud.DbInstance.Instance.Size.ToList<Size>();
        internal List<Crust> crust = Crud.DbInstance.Instance.Crust.ToList<Crust>();

        public List<Ingredients> currentingredients = new List<Ingredients>();
        public List<Inventory> currentinventory = new List<Inventory>();
        public List<IndivPizza> orders = new List<IndivPizza>();

        public List<string> showcrust = new List<string>();
        public List<string> ingredient = new List<string>();

        //ing0-ing4, crustFID, sizeFID, Count, OrderFID, cost
        IndivPizza ipz = new IndivPizza();
        Ingredients ing = new Ingredients();
        Inventory inv = new Inventory();
        Crust cr = new Crust();
        Size sz = new Size();
        Orderpizza ord = new Orderpizza();
        AccountLogic uu = new AccountLogic();
        UserInfo ui = new UserInfo();


        public int sendCountToQ(string input) // would have to dynamically change max value to account for both
                                               //inventory and current order count
        {
            if (Int32.TryParse(input, out int number))
            {
                if (number < 100 && number > 0)
                {
                    ipz.Count = number;
                    return number;
                }
                else
                {
                    Console.WriteLine("insert a number 1-100:");
                    return sendCountToQ(Console.ReadLine());
                }
            }
            else
            {
                Console.WriteLine("Insert a number 1-100:");
                return sendCountToQ(Console.ReadLine());
            }
        }
        public void setlocationFID(int num)
        {
            this.locationFID = num;
        }
        public void setusernameid(int input)
        {
            this.userFID = input;
        }
        public void resetIngID()
        {
            this.ing0 = "none";
            this.ing1 = "none";
            this.ing2 = "none";
            this.ing3 = "none";
            this.ing4 = "none";
        }        
        public void setOrderID()
        {
            ord.LocationFid = this.locationFID;
            ord.UserFid = this.userFID;
        }
        public void sendOrderFIDtoQ()
        {
            ipz.OrderFid = this.OrderFID;
        }
        public Ingredients returnsIngredientsObj(string ing)
        {
            Ingredients i = Crud.DbInstance.Instance.Ingredients.Where<Ingredients>(z => z.Ingredient == ing).FirstOrDefault<Ingredients>();

            if (i == null)
            {
                Console.WriteLine("Fail to obtain ingredient, re-enter");
                return returnsIngredientsObj(Console.ReadLine().ToLower());
            }
            else if (i.Ingredient == ing)
            {
                return i;
            }
            else
                return returnsIngredientsObj(Console.ReadLine().ToLower());
        }

        //---------------------------------------
        public void setThisIngredientsString()
        {
            foreach(Ingredients i in currentingredients)
            {
                ingredient.Add(i.Ingredient);
            }
        }
        public void setThisIngredientsList()
        {
            foreach(Inventory i in currentinventory)
            {
                currentingredients.Add(Crud.DbInstance.Instance.Ingredients.Where<Ingredients>(x => x.IngId == i.FkIngredient && i.Resfid ==locationFID).FirstOrDefault<Ingredients>());
            }
        }
        public void setThisInventory()
        {
            currentinventory = Crud.DbInstance.Instance.Inventory.Where<Inventory>(x => x.Resfid == this.locationFID).ToList<Inventory>();
        }
        public void sendIngredientstoQ()
        {
            ing = toppings.Where<Ingredients>(x => x.Ingredient == this.ing0).FirstOrDefault<Ingredients>();
            ipz.Ingredient0Fid = ing.IngId;
            ing = toppings.Where<Ingredients>(x => x.Ingredient == this.ing1).FirstOrDefault<Ingredients>();
            ipz.Ingredient1Fid = ing.IngId;
            ing = toppings.Where<Ingredients>(x => x.Ingredient == this.ing2).FirstOrDefault<Ingredients>();
            ipz.Ingredient2Fid = ing.IngId;
            ing = toppings.Where<Ingredients>(x => x.Ingredient == this.ing3).FirstOrDefault<Ingredients>();
            ipz.Ingredient3Fid = ing.IngId;
            ing = toppings.Where<Ingredients>(x => x.Ingredient == this.ing4).FirstOrDefault<Ingredients>();
            ipz.Ingredient4Fid = ing.IngId;
            //Crud.DbInstance.Instance.Add(ing);
        }

        //----------------------------------------
        public void showTop(List<Ingredients> currentingredients)
        {
            foreach(Ingredients ing in currentingredients)
            {
                Console.WriteLine($"){ing.Ingredient} | price: {ing.Cost}");
            }
        }
        public void setLocalStorageTop(string input, int count)
        {
            if (count == 0)
                ing0 = input;
            else if (count == 1)
                ing1 = input;
            else if (count == 2)
                ing2 = input;
            else if (count == 3)
                ing3 = input;
            else if (count == 4)
                ing4 = input;
            else
            {
                Console.WriteLine("default: too many toppings");
            }
            
        }
        //public decimal? topPrice(int ingid)
        //{
        //    decimal? price;
        //    price = toppings[ingid].Cost;
        //    return price

        //}


//------------------------------------------------------------Deals with crust of order
        public void setLocalStorageCrust(string input)
        {
            cr = crust.Where<Crust>(x => x.Crust1 == input).FirstOrDefault<Crust>();
            if (cr == null)
            {
                Console.WriteLine("select existing crust");
                setLocalStorageCrust(Console.ReadLine().ToLower());
            }
        }
        public void sendCrusttoQ()
        {
            ipz.CrustFid = cr.CrustId;
        } //can edit to input number according to location in list ... for multiple orders before q'ing
        public void showCrust()
        {
            foreach (Crust cr in crust) //shows existing toppings and removes chosen topping.
            {
                Console.WriteLine($"){cr.Crust1} | price: {cr.Totalcost}");
            }
        }
        public decimal? crustPrice()
        {
            return this.cr.Totalcost;
        }
//------------------------------------------------------------Deals With size of Order
        public void setLocStorageSize(string input)

        {
            sz = size.Where<Size>(x => x.Size1 == input).FirstOrDefault<Size>();
            if (sz == null)
            {
                Console.WriteLine("select existing size");
                setLocStorageSize(Console.ReadLine().ToLower());
            }

        }
        public void sendSizetoQ()
        {
            ipz.SizeFid = sz.SizeId;
        } 
        public void showSize()
        {

            foreach (Size sz in size) //shows existing toppings and removes chosen topping.
            {
                Console.WriteLine(")" + sz.Size1 + $"| price: {sz.Totalcost} ");
            }
        }
        public decimal? sizePrice()
        {
            return this.sz.Totalcost;
        }
//---------------------------------------------------------------VVVVV---NEEDSREWORK
        public void setCost(decimal? price) //price of individual order, only includes count, cannot include SECOND order
        {
            ipz.Totalcost = price +ipz.Totalcost;
        }
        public void showCost()
        {
            Console.WriteLine(ipz.Totalcost);
        }
        public void initializeCost()
        {
            ipz.Totalcost = 0;
        }

//--------------------------------------------------------------------VVV---NEEDSREWOROK
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
            Crud.DbInstance.Instance.IndivPizza.Add(fromPizzaBuild(0));
            Crud.DbInstance.Instance.Orderpizza.Add(ord);
            Crud.DbInstance.Instance.SaveChanges();
        }
    }

}
