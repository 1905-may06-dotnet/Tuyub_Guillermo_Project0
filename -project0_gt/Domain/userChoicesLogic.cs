using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class userChoicesLogic
    {
        string input;
        int countTop = 0;
        decimal? currentcost = 0;
        decimal? totalcost = 0;
        PizzaLogic pl = new PizzaLogic();
        Ingredients ingredient = new Ingredients();
        IndivPizza pizzaorder = new IndivPizza();

       // List<Ingredients> ingredientInUserChoicesLogic = new List<Ingredients>();

        Size size = new Size();
        Crust crust = new Crust();
        Inventory invent = new Inventory();
        public bool choices(List<Ingredients> currentIngredients)
        {
            //ingredientInUserChoicesLogic = currentIngredients;
            Console.WriteLine($"$5.99 Medium {currentIngredients[0].Ingredient} with {currentIngredients[1].Ingredient} pizza: one");
            Console.WriteLine($"$5.99 Medium {currentIngredients[2].Ingredient} with {currentIngredients[3].Ingredient}: two");
            Console.WriteLine("Customize order: custom");

            input = Console.ReadLine().ToLower();
            customize(currentIngredients);

            Console.WriteLine($"price of current pizza:");
            currentCost();
            Console.WriteLine("How many of this pizza:");
                input = Console.ReadLine();
                int a = pl.sendCountToQ(input);
                totalcost = totalcost + currentcost * (a ); //need to change so it only multiplies CURRENT pizza,not whole order
                                                             //logic would be only updating FULLcost at end, and CURRENT cost until then.
                pl.setCost(totalcost);
                pl.showCost();
            return orderMore();
        }

        public bool customize(List<Ingredients> currentIngredients)
        {

            if (input.Equals("custom")) //create case statements to verify input ... later finish implementation first.
            {

                Console.WriteLine("\t\t What size pizza?");
                pl.showSize();
                input = Console.ReadLine().ToLower();
                pl.setLocStorageSize(input);
                currentcost = pl.sizePrice() + currentcost;
                currentCost();
                
                Console.WriteLine("\t\t Which crust would you like?");
                pl.showCrust();
                input = Console.ReadLine().ToLower();
                pl.setLocalStorageCrust(input);
                currentcost = pl.crustPrice() + currentcost;
                currentCost();
                
                Console.WriteLine("\t\t What topping would you like?"); // toppings can be cleaner, works for now
                pl.showTop(currentIngredients);
                input = Console.ReadLine().ToLower();
                ingredient = pl.returnsIngredientsObj(input);
                pl.setLocalStorageTop(input, countTop);
                currentcost = ingredient.Cost + currentcost;
                countTop++;
                currentCost();

                return toppingChooser(currentIngredients);

            }
            else if (input.Equals("one"))
            {

                pl.setLocStorageSize("medium");

                pl.setLocalStorageCrust("regular");

                pl.setLocalStorageTop(currentIngredients[0].Ingredient, countTop);
                countTop++;
                pl.setLocalStorageTop(currentIngredients[1].Ingredient, countTop);
                countTop++;

                currentcost = 5.99M + currentcost;
                currentCost();
                pl.setCost(currentcost);
                Console.WriteLine("\t\tContinuing to Cart");
                return true;

            } //No maintains default-> cart
            else if (input.Equals("two"))
            {
                pl.setLocStorageSize("medium");

                pl.setLocalStorageCrust("regular");

                pl.setLocalStorageTop(currentIngredients[2].Ingredient, countTop);
                countTop++;
                pl.setLocalStorageTop(currentIngredients[3].Ingredient, countTop);
                countTop++;

                currentcost = 5.99M + currentcost;
                currentCost();
                pl.setCost(currentcost);
                Console.WriteLine("\t\tContinuing to Cart");
                return true;

            }
            else
            {
                Console.WriteLine("\t\tPlease insert custom, one, or two");
                input = Console.ReadLine().ToLower();
                return customize(currentIngredients);
            } //recursive to userinput

        }
        public bool checkcustomize(List<Ingredients> currentIngredients, bool input) //right after user inputs y/n for another pizza
        {
            string strinput;

            if (input == false)
            {
                //is supposed to input order into one LIST of orders
                // and upload orders once done.... currently obj exception error
                //count already sent
                pl.sendIngredientstoQ(); //ing0-4
                pl.sendSizetoQ();//sizefid
                pl.sendCrusttoQ();//crustfid
                pl.setOrderID();
                pl.sendOrderFIDtoQ(); //orderfid

                //crustfid,sizefid,count,orderfid,cost
                pizzaorder = pl.passIndivPizza();
                pl.addPizzaBuild(pizzaorder);
                countTop = 0;
                //reset ingredient for show


                return choices(currentIngredients);
            }
            else if (input == true)
            {
                pl.setCost(currentcost);
                Console.WriteLine("price of current order:");
                pl.showCost();
                //show current order as well... requires order object

                Console.WriteLine("send order? y:yes | n: no to cancel order");
                strinput = Console.ReadLine().ToLower();
                return sendorcancel();
                bool sendorcancel()
                {
                    if (strinput == "y")
                    {
                        pl.sendIngredientstoQ(); //ing0-4
                        pl.sendSizetoQ();//sizefid
                        pl.sendCrusttoQ();//crustfid
                        pl.setOrderID();
                        pl.sendOrderFIDtoQ(); //orderfid

                        //crustfid,sizefid,count,orderfid,cost
                        pizzaorder = pl.passIndivPizza();
                        pl.addPizzaBuild(pizzaorder);
                        countTop = 0;

                        pl.setCost(currentcost); //add current to total cost
                        pl.sendPizzaBuild();
                        pl.resetIngID();
                        Console.WriteLine("Order Confirmed");
                        return choices(currentIngredients);
                        //command to upload info to database;
                    }
                    else if (strinput == "n")
                    {
                        Console.WriteLine("cancelling order");
                        if (pl.passIndivPizza().Count != null)
                            pl.rmallPizzaBuild();
                        countTop = 0;
                        currentcost = 0;
                        return choices(currentIngredients);
                    }
                    else
                    {
                        Console.WriteLine("enter y or n");
                        strinput = Console.ReadLine();
                        return sendorcancel();
                    }
                }
            }
            else
            {
                Console.WriteLine("enter y:yes to add to order or n:no to cancel ");
                strinput = Console.ReadLine().ToLower();
                return checkcustomize(currentIngredients,input);
            }
            //choices(currentIngredients);
        }
        bool toppingChooser(List<Ingredients> currentIngredients)
        {
            Console.WriteLine("\t\tAdd another topping? y for yes, n for no");
            input = Console.ReadLine().ToLower();

            if (input.Equals("y") || input.Equals("yes")) //add additional topping
            {
                Console.WriteLine("\t\tWhat topping would you like to add?");

                pl.showTop(currentIngredients);
                input = Console.ReadLine().ToLower();
                ingredient = pl.returnsIngredientsObj(input);
                pl.setLocalStorageTop(input, countTop);
                currentcost = ingredient.Cost + currentcost;
                countTop++;
               return addTopping(currentIngredients);
            }
            else if (input.Equals("n") || input.Equals("no"))
            {// if y is n, continue;
                Console.WriteLine($"{countTop} topping chosen");
                pl.setCost(currentcost);
                return false;
            }
            else
            {
                Console.WriteLine("please insert y for yes, n for no");
                return toppingChooser(currentIngredients);
            }
        }
        bool addTopping(List<Ingredients> currentIngredients)
        {
            if (countTop < 5)
            {
                Console.WriteLine("would you like to add another topping? Max 5 toppings");
                Console.WriteLine($"at {countTop} toppings");
                input = Console.ReadLine().ToLower();
            }

            if ((input.Equals("yes") || input.Equals("y")))
            {
                return toppingChooser(currentIngredients);
            }
            else if (countTop >= 5)
            {
                Console.WriteLine("continuing to cart");
                //pl.setCost(currentcost);
                return false;
            }
            else if (input.Equals("no") || input.Equals("n"))
            {
                Console.WriteLine("continuing to cart");
                //pl.setCost(currentcost);
                return false;
            }
            else
            {
                Console.WriteLine("Please insert y for yes, n for no");
               return addTopping(currentIngredients);

            }
        }
        internal void currentCost()
        {
            Console.WriteLine($"current cost is: {currentcost}");
        }
        bool orderMore()
        {
            Console.WriteLine("would you like to order another pizza?");
            input = Console.ReadLine().ToLower();
            if (input == "y")
                return false;
            else if (input == "n")
            {
                return true;
            }
            else
            {

                return orderMore();

            }

        }

    }
}
