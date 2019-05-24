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

        public List<Ingredients> currentingredients = new List<Ingredients>();
        public List<Inventory> currentinventory = new List<Inventory>();
        public List<IndivPizza> orders = new List<IndivPizza>();

        public List<string> crust = new List<string>();
        public List<string> showcrust = new List<string>();
        public List<string> size = new List<string>();
        public List<string> showsize = new List<string>();
        public List<string> ingredient = new List<string>();
        //ing0-ing4, crustFID, sizeFID, Count, OrderFID, cost

        IndivPizza ipz = new IndivPizza();
        Ingredients ing = new Ingredients();
        Inventory inv = new Inventory();
        Crust cr = new Crust();
        Size sz = new Size();
        Orderpizza ord = new Orderpizza();
        User uu = new User();
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
                    sendCountToQ(Console.ReadLine());
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
        public void setusername(string input)
        {
            this.username = input;
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
            ui = Crud.DbInstance.Instance.UserInfo.Where<UserInfo>(x => x.UserName == this.username).FirstOrDefault<UserInfo>();
            ord.UserFid = ui.UserId;
            Orderpizza ord2 = Crud.DbInstance.Instance.Orderpizza.Where<Orderpizza>(x => x.LocationFid == this.locationFID).LastOrDefault<Orderpizza>();

            this.OrderFID = ord2.OrderId;
        }
        public void sendOrderFIDtoQ()
        {
            ipz.OrderFid = this.OrderFID;
        }
        public string getSentIngredient() // only returns after locationfid and ingid is set
        {
            inv = Crud.DbInstance.Instance.Inventory.Where<Inventory>(u => u.Resfid == this.locationFID && u.FkIngredient == this.ingFID).FirstOrDefault<Inventory>();
            ing = Crud.DbInstance.Instance.Ingredients.Where<Ingredients>(y => y.IngId == this.ingFID).FirstOrDefault<Ingredients>();
            return ing.Ingredient;
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
                currentingredients.Add(Crud.DbInstance.Instance.Ingredients.Where<Ingredients>(x => x.IngId == i.FkIngredient && i.Resfid ==this.locationFID).FirstOrDefault<Ingredients>());
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
        public void commitToDb()
        {
            Crud.DbInstance.Instance.SaveChanges();
        }


        public void showTop()
        {
            foreach (Inventory inv in inventories)
            {
                if (inv.Resfid == this.locationFID) {
                    foreach (Ingredients str in toppings) //shows existing toppings and removes chosen topping.
                    {   if(str.IngId==inv.FkIngredient)
                            Console.WriteLine(")" + str.Ingredient + $"|price: {str.Cost}");
                    }
                }
            }
        }
        public void setUserTop(string input, int count)
        {
            if (count == 0)
                this.ing0 = input;
            else if (count == 1)
                this.ing1 = input;
            else if (count == 2)
                this.ing2 = input;
            else if (count == 3)
                this.ing3 = input;
            else if (count == 4)
                this.ing4 = input;
            else
            {
                Console.WriteLine("default: too many toppings");
            }
            
        }
        public string getUserTop(int count)
        {
            if (count == 0)
                return this.ing0;
            else if (count == 1)
                return this.ing1;
            else if (count == 2)
                return this.ing2;
            else if (count == 3)
                return this.ing3;
            else if (count == 4)
                return this.ing4;
            else
            {
                Console.WriteLine("returning null");
                return null;
            }
        }


//------------------------------------------------------------Deals with crust of order
        public Crust getCrust(int number)
        {
            Crust ct = Crud.DbInstance.Instance.Crust.Where<Crust>(u => u.CrustId == number).FirstOrDefault<Crust>();
            return ct;
        }
        public Crust getCrust(string name)
        {
            Crust cr = Crud.DbInstance.Instance.Crust.Where<Crust>(u => u.Crust1 == name).FirstOrDefault<Crust>();
            if (cr == null)
            {
                //Count--;
                Console.WriteLine("Fail to obtain Cost, re-enter crust name");
               return getCrust(Console.ReadLine().ToLower());
            }
            else if (cr.Crust1== name)
            {
                return cr;
            }
            else
                return getCrust(Console.ReadLine().ToLower());
        }
        public void setCrust(string input)
        {
            crust.Add(input);
        }
        public void sendCrusttoQ()
        {
            cr = Crud.DbInstance.Instance.Crust.Where<Crust>(x => x.Crust1 == this.crust[0]).FirstOrDefault<Crust>();
            ipz.CrustFid = cr.CrustId;
        } //can edit to input number according to location in list ... for multiple orders before q'ing
        public void setshowCrust(string input)
        {
            showcrust.Add(input);
        }
        public void showCrust()
        {
            foreach (string str in showcrust) //shows existing toppings and removes chosen topping.
            {
                Crust ct = Crud.DbInstance.Instance.Crust.Where<Crust>(u => u.Crust1 == str).FirstOrDefault<Crust>();
                Console.WriteLine(")" + str + $"| price: {ct.Totalcost}");
            }
        }



//------------------------------------------------------------Deals With size of Order
        public Size getSize(int number)
        {
            Size sz = Crud.DbInstance.Instance.Size.Where<Size>(u => u.SizeId == number).FirstOrDefault<Size>();
            return sz;
        }
        public Size getSize(string name)
        {
            Size sz = Crud.DbInstance.Instance.Size.Where<Size>(u => u.Size1 == name).FirstOrDefault<Size>();
            if (sz == null)
            {
                //Count--;
                Console.WriteLine("Fail to obtain Cost, re-enter size name");
                return getSize(Console.ReadLine().ToLower());
            }
            else if (sz.Size1 == name)
            {
                return sz ;
            }
            else
                return getSize(Console.ReadLine().ToLower());
        }
        public void setSize(string input)

        {
            size.Add(input);
        }
        public void sendSizetoQ()
        {
            sz = Crud.DbInstance.Instance.Size.Where<Size>(x => x.Size1 == this.size[0]).FirstOrDefault<Size>();
            ipz.SizeFid = sz.SizeId;
        } //can edit to input number according to location in list ... for multiple orders before q'ing
        public void setshowSize(string input)
        {
            showsize.Add(input);
        }
        public void showSize()
        {

            foreach (string str in showsize) //shows existing toppings and removes chosen topping.
            {
                Size sz = Crud.DbInstance.Instance.Size.Where<Size>(u => u.Size1 == str).FirstOrDefault<Size>();
                Console.WriteLine(")" + str + $"| price: {sz.Totalcost} ");
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
