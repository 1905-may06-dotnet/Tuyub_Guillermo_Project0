using System;
using Data.Models;
using Domain;

namespace Client

{
    public class Main_Program
    {
        public static void Main(string[] args)
        {

            string input;
            int countTop = 0;
            decimal? currentcost = 0;

            //front-end:user
            User uu = new User();

            //front-end:location
            ResLocation res = new ResLocation();

            //front-end:user
            PizzaLogic user_pizza = new PizzaLogic();

            //holds database info
            RestaurantLogic rl = new RestaurantLogic();
            PizzaLogic pl = new PizzaLogic();

            IndivPizza pizzaorder = new IndivPizza();


            Ingredients ingredient = new Ingredients();
            Size size = new Size();
            Crust crust = new Crust();
            int i = 1;
            while (pl.getIngredients(i) != null)
            {
                ingredient = pl.getIngredients(i);
                pl.setTop(ingredient.Ingredient);
                i++;
            }
            int j = 1;
            while (pl.getSize(j) != null)
            {
                size = pl.getSize(j);
                pl.setSize(size.Size1);
                j++;
            }
            int k = 1;
            while (pl.getCrust(k) != null)
            {
                crust = pl.getCrust(k);
                pl.setCrust(crust.Crust1);
                k++;
            }


            userInput();


            void userInput(){

                Console.WriteLine("Sign in: S | Register: R");  //sign in/register

                uu.SignOrRegister(Console.ReadLine().ToLower());


                Console.WriteLine("Choose Restaurant: ");
                Console.WriteLine("1) Dominos, CA \n2) PizzaHut, CA \n3) Gaspare's,CA \n4) Nizarrio's, LA" +
                    "\n5) Dominos, RE \n6) PizzaHut, RE");

                //holds locationid,state,city,zipcode,resname
                //use locationid as foreign key to call other tables

                res = rl.chooseRestaurant(Console.ReadLine());

                Console.WriteLine("\t\tChosen: " + res.ResName);

                choices();


            }
            void choices()
            {
                Console.WriteLine("$5.99 Medium Pepperoni with bacon pizza: one");
                Console.WriteLine("$5.99 Medium Pineapple with ham: two");
                Console.WriteLine("Customize order: custom");

                input = Console.ReadLine().ToLower();
                customize();

                Console.WriteLine($"price of current pizza:");
                user_pizza.showCost();

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
                    pl.showSize();
                    input = Console.ReadLine().ToLower();
                    user_pizza.setSize(input);
                    currentcost = user_pizza.getSize(input).Totalcost + currentcost;

                    Console.WriteLine("\t\t Which crust would you like?");
                    pl.showCrust();
                    input = Console.ReadLine().ToLower();
                    user_pizza.setCrust(input);
                    currentcost = user_pizza.getCrust(input).Totalcost + currentcost;

                    Console.WriteLine("\t\t What topping would you like?");
                    pl.showTop();
                    input = Console.ReadLine().ToLower();
                    user_pizza.setTop(input);
                    currentcost = user_pizza.obtainTopObj(input).Cost + currentcost;

                    pl.rmTop(input);
                    countTop++;

                    Console.WriteLine("\t\tAdd another topping?");
                    input = Console.ReadLine().ToLower();
                    toppingChooser();

                } //yes continues to options
                else if (input.Equals("one"))
                {
                    user_pizza.setSize("medium");

                    user_pizza.setCrust("regular");

                    user_pizza.setTop("pepperoni", "bacon");

                    pl.setIngredient(ingredient, 1);

                    pl.setIngredient(ingredient, 2);

                    currentcost = 5.99M + currentcost;
                    user_pizza.setCost(currentcost);
                    Console.WriteLine("\t\tContinuing to Cart");
                    //checkcustomize();

                } //No maintains default-> cart
                else if (input.Equals("two"))
                {
                    user_pizza.setSize("medium");

                    user_pizza.setCrust("regular");

                    user_pizza.setTop("pineapple");
                    user_pizza.setTop("ham");
                    ingredient = user_pizza.obtainTopObj ("pineapple");
                    pl.setIngredient(ingredient, 1);

                    ingredient = user_pizza.obtainTopObj("ham");
                    pl.setIngredient(ingredient, 2);

                    currentcost = 5.99M + currentcost;
                    user_pizza.setCost(currentcost);
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
                  pizzaorder = user_pizza.passIndivPizza();
                   // user_pizza.addPizzaBuild(pizzaorder); ----
                  

                    //input = "custom"; //hardcode to get customize to run proper
                    choices();
                }
                else if (input == "no" || input == "n")
                {
                    Console.WriteLine("price of current order:");
                    user_pizza.showCost();
                    Console.WriteLine("send order? y:yes | n: no to cancel order");
                    input = Console.ReadLine().ToLower();
                    sendorcancel();
                    void sendorcancel(){
                        if (input == "y")
                        {
                            Console.WriteLine("order sent");
                            //command to upload info to database;
                        }
                        else if (input == "n")
                        {
                            Console.WriteLine("cancelling order");
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

                    input = Console.ReadLine().ToLower(); //should check for topping if allowed
                    user_pizza.setTop(input);
                    currentcost = user_pizza.obtainTopObj(input).Cost + currentcost;
                    user_pizza.setCost(currentcost);
                    pl.rmTop(input);
                    countTop++;
                    addTopping();
                }
                else if (input.Equals("n") || input.Equals("no"))   
                {// if y is n, continue;
                    Console.WriteLine("n topping chosen: continuing to cart");
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

                if((input.Equals("yes") || input.Equals("y")))
                {
                    toppingChooser();
                }
                else if(countTop >= 5)
                {
                    Console.WriteLine("continuing to cart");
                }
                else if(input.Equals("no") || input.Equals("n"))
                {
                    Console.WriteLine("continuing to cart");
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
