using System;
using System.Collections.Generic;
using Data.Models;
using Domain;

namespace Client

{
    public class Main_Program
    {
        public static void Main(string[] args)
        {

            string          input;
            int             countTop = 0;
            decimal?        currentcost = 0;

            User            uu = new User();
            ResLocation     res = new ResLocation();
            RestaurantLogic rl = new RestaurantLogic();
            PizzaLogic      pl = new PizzaLogic();
            IndivPizza      pizzaorder = new IndivPizza();
            Ingredients     ingredient = new Ingredients();
            Size            size = new Size();
            Crust           crust = new Crust();
            Inventory       invent = new Inventory();



            int j = 1;
            while (pl.getSize(j) != null)
            {
                size = pl.getSize(j);
                pl.setshowSize(size.Size1);
                j++;
            }
            int k = 1;
            while (pl.getCrust(k) != null)
            {
                crust = pl.getCrust(k);
                pl.setshowCrust(crust.Crust1);
                k++;
            }


            userInput();


            void userInput(){


                Console.WriteLine("Sign in: S | Register: R");  //sign in/register
                input = Console.ReadLine().ToLower();
                uu.SignOrRegister(input);
                pl.setusername(uu.getUName());

                Console.WriteLine("Choose Restaurant: ");
                rl.showLocation();
                res=rl.chooseRestaurant(Console.ReadLine());
                pl.setlocationFID(res.LocationId);
                pl.setThisInventory();
                pl.setThisIngredientsList();
                pl.setThisIngredientsString();

                Console.WriteLine("\t\tChosen: " + res.ResName);

                choices();


            }
            void choices()
            {
                
                Console.WriteLine($"$5.99 Medium {pl.ingredient[0]} with {pl.ingredient[1]} pizza: one");
                Console.WriteLine($"$5.99 Medium {pl.ingredient[2]} with {pl.ingredient[3]}: two");
                Console.WriteLine("Customize order: custom");

                input = Console.ReadLine().ToLower();
                customize();

                Console.WriteLine($"price of current pizza:");
                pl.showCost();
                Console.WriteLine("How many of this pizza:");
                input = Console.ReadLine();
                int a = pl.sendCountToQ(input);
                currentcost = currentcost * a; //need to change so it only multiplies CURRENT pizza,not whole order
                //logic would be only updating FULLcost at end, and CURRENT cost until then.
                pl.setCost(currentcost);
                pl.showCost();
                Console.WriteLine("Add another pizza?");
                Console.WriteLine("y: yes || n:no");
                input = Console.ReadLine().ToLower();
                checkcustomize();
            }
            void customize()
            {
                if (input.Equals("custom")) //create case statements to verify input ... later finish implementation first.
                {
                    Console.WriteLine("\t\t What size pizza?");
                    pl.showSize();                          //ought to show price as well
                    input = Console.ReadLine().ToLower();
                    pl.setSize(input);
                    currentcost = pl.getSize(input).Totalcost + currentcost;

                    Console.WriteLine("\t\t Which crust would you like?");
                    pl.showCrust();
                    input = Console.ReadLine().ToLower();
                    pl.setCrust(input);
                    currentcost = pl.getCrust(input).Totalcost + currentcost;

                    Console.WriteLine("\t\t What topping would you like?");
                    pl.showTop();
                    input = Console.ReadLine().ToLower();
                    ingredient = pl.returnsIngredientsObj(input);
                    pl.setUserTop(input,countTop);
                    currentcost = ingredient.Cost + currentcost;
                    countTop++;

                    Console.WriteLine("\t\tAdd another topping?");
                    input = Console.ReadLine().ToLower();
                    toppingChooser();

                } 
                else if (input.Equals("one"))
                {
                    
                    pl.setSize("medium");

                    pl.setCrust("regular");

                    pl.setUserTop(pl.ingredient[0], countTop);
                    countTop++;
                    pl.setUserTop(pl.ingredient[1], countTop);
                    countTop++;

                    currentcost = 5.99M + currentcost;
                    pl.setCost(currentcost);
                    Console.WriteLine("\t\tContinuing to Cart");
                    //checkcustomize();

                } //No maintains default-> cart
                else if (input.Equals("two"))
                {
                    pl.setSize("medium");

                    pl.setCrust("regular");

                    pl.setUserTop(pl.ingredient[2],countTop);
                    countTop++;
                    pl.setUserTop(pl.ingredient[3], countTop);
                    countTop++;

                    currentcost = 5.99M + currentcost;
                    pl.setCost(currentcost);
                    Console.WriteLine("\t\tContinuing to Cart");

                   // checkcustomize();
                }
                else
                {
                    Console.WriteLine("\t\tPlease insert custom, one, or two");
                    input = Console.ReadLine().ToLower();
                    customize();
                } //recursive to userinput

            }
            void checkcustomize() //right after user inputs y/n for another pizza
            {
                
                if (input == "yes" || input == "y")
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
                    //reset ingredient/crust/size for show

                    choices();
                }
                else if (input == "no" || input == "n")
                {
                    Console.WriteLine("price of current order:");
                    pl.showCost();
                    //show current order as well... requires order object
                    
                    Console.WriteLine("send order? y:yes | n: no to cancel order");
                    input = Console.ReadLine().ToLower();
                    sendorcancel();
                    void sendorcancel(){
                        if (input == "y")
                        {
                            Console.WriteLine("order sent");
                            pl.sendIngredientstoQ(); //ing0-4
                            pl.sendSizetoQ();//sizefid
                            pl.sendCrusttoQ();//crustfid
                            pl.setOrderID();
                            pl.sendOrderFIDtoQ(); //orderfid

                            //crustfid,sizefid,count,orderfid,cost
                            pizzaorder = pl.passIndivPizza();
                            pl.addPizzaBuild(pizzaorder);
                            countTop = 0;

                            pl.setCost(currentcost);
                            pl.sendPizzaBuild();
                            //pl.commitToDb();
                            pl.resetIngID();
                            userInput();
                            //command to upload info to database;
                        }
                        else if (input == "n")
                        {
                            Console.WriteLine("cancelling order");
                            if(pl.passIndivPizza().Count != null)
                                pl.rmallPizzaBuild();
                            countTop = 0;
                            currentcost = 0;
                            userInput();
                        }
                        else
                        {
                            Console.WriteLine("enter y or n");
                            input = Console.ReadLine();
                            sendorcancel();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("enter y:yes to add to order or n:no to ");
                    input = Console.ReadLine().ToLower();
                    checkcustomize();
                }
            }
            void toppingChooser()
            {
                if (input.Equals("y") || input.Equals("yes")) //add additional topping
                {
                    Console.WriteLine("\t\tWhat topping would you like to add?");

                    pl.showTop();
                    input = Console.ReadLine().ToLower();
                    ingredient = pl.returnsIngredientsObj(input);
                    pl.setUserTop(input, countTop);
                    currentcost = ingredient.Cost + currentcost;
                    countTop++;
                    addTopping();
                }
                else if (input.Equals("n") || input.Equals("no"))   
                {// if y is n, continue;
                    Console.WriteLine("n topping chosen: continuing to cart");
                    pl.setCost(currentcost);
                }
                else
                {
                    Console.WriteLine("please insert y for yes, n for no");
                    input = Console.ReadLine().ToLower();
                    toppingChooser();
                }
            }
            void addTopping()
            {
                if (countTop < 5)
                {
                    Console.WriteLine("would you like to add another topping? Max 5 toppings");
                    Console.WriteLine($"at {countTop} toppings");
                    input = Console.ReadLine().ToLower();
                }

                if ((input.Equals("yes") || input.Equals("y")))
                {
                    toppingChooser();
                }
                else if (countTop >= 5)
                {
                    Console.WriteLine("continuing to cart");
                }
                else if (input.Equals("no") || input.Equals("n"))
                {
                    Console.WriteLine("continuing to cart");
                    pl.setCost(currentcost);
                }
                else
                {
                    Console.WriteLine("Please insert y for yes, n for no");
                    addTopping();

                }
            }
        }
    }
}
