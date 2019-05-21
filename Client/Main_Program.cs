using System;
using Data.Models;
using Domain;

namespace Client

{
    class Main_Program
    {
        static void Main(string[] args)
        {
            string input;
            int countTop = 0;

            //front-end:user
            User uu = new User();

            //front-end:location
            ResLocation res= new ResLocation();

            //holds database info
            RestaurantLogic rl = new RestaurantLogic();
            PizzaLogic pl = new PizzaLogic();

            //Use this object to link to inventory
            //PizzaLogic pp = new PizzaLogic();

            //Use this object to link to user
            PizzaLogic user_pizza = new PizzaLogic();

            Ingredients ingredient = new Ingredients();
            Size size = new Size();
            Crust crust = new Crust();


            Console.WriteLine("Sign in: S | Register: R");  //sign in/register

            uu.SignOrRegister(Console.ReadLine().ToLower());


            Console.WriteLine("Choose Restaurant: ");
            Console.WriteLine("1) Dominos, CA \n2) PizzaHut, CA \n3) Gaspare's,CA \n4) Nizarrio's, LA" +
                "\n5) Dominos, RE \n6) PizzaHut, RE");

     //holds locationid,state,city,zipcode,resname
     //use locationid as foreign key to call other tables

            res = rl.chooseRestaurant(Console.ReadLine());

            Console.WriteLine("\t\tChosen: " + res.ResName);

            int i = 1;
            while(pl.getIngredients(i) != null)
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

            Console.WriteLine("$5.99 Medium Pepperoni with bacon pizza: one");
            Console.WriteLine("$5.99 Medium Pineapple with ham: two");
            Console.WriteLine("Customize order: custom");

            input = Console.ReadLine().ToLower();

            userInput(); //initial userinput

            void userInput(){
                
                if (input.Equals("custom")) //create case statements to verify input ... later finish implementation first.
                {     
                    Console.WriteLine("\t\t What size pizza?");
                    pl.showSize();
                    input = Console.ReadLine().ToLower();
                    user_pizza.setSize(input);

                    Console.WriteLine("\t\t Which crust would you like?");
                    pl.showCrust();
                    input = Console.ReadLine().ToLower();
                    user_pizza.setCrust(input);

                    Console.WriteLine("\t\t What topping would you like?");
                    pl.showTop();
                    input = Console.ReadLine().ToLower();
                    user_pizza.setTop(input);
                    pl.rmTop(input);
                    countTop++;

                    Console.WriteLine("\t\tAdd another topping?");
                    input = Console.ReadLine().ToLower();
                    toppingChooser();

                } //yes continues to options
                else if(input.Equals("one"))
                {
                    decimal? x, y;
                    user_pizza.setSize("medium");
                    user_pizza.setCrust("regular");
                    user_pizza.setTop("pepperoni", "bacon");
                    ingredient = user_pizza.obtainCost("pepperoni");
                    x = ingredient.Cost;
                    pl.setIngredient(ingredient,1);

                    ingredient = user_pizza.obtainCost("bacon");
                    y = ingredient.Cost;
                    pl.setIngredient(ingredient,2);

                    user_pizza.setCost(x+y);

                    Console.WriteLine("\t\tContinuing to Cart");

                } //No maintains default-> cart
                else if (input.Equals("two"))
                {
                    decimal? x, y;
                    user_pizza.setSize("medium");
                    user_pizza.setCrust("regular");
                    user_pizza.setTop("pineapple");
                    user_pizza.setTop("ham");
                    ingredient=user_pizza.obtainCost("pineapple");
                    x = ingredient.Cost;
                    pl.setIngredient(ingredient,1);

                    ingredient = user_pizza.obtainCost("ham");
                    y = ingredient.Cost;
                    pl.setIngredient(ingredient,2);

                    user_pizza.setCost(x + y);

                    Console.WriteLine("\t\tContinuing to Cart");

                }
                else
                {
                    Console.WriteLine("\t\tPlease insert y for yes, n for no");
                    input = Console.ReadLine().ToLower();
                    userInput();
                } //recursive to userinput


            }
            void toppingChooser()
            {
                if (input.Equals("y") || input.Equals("yes")) //add additional topping
                {
                    Console.WriteLine("\t\tWhat topping would you like to add?");

                    pl.showTop();

                    input = Console.ReadLine().ToLower(); //should check for topping if allowed
                    //if input == existing topping in pp, add in user_pizza
                    user_pizza.setTop(input);
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

                Console.WriteLine("would you like to add another topping? Max 5 toppings");
                Console.WriteLine($"at {countTop} toppings");
                input = Console.ReadLine().ToLower();

                if((input.Equals("yes") || input.Equals("y")) && countTop<5)
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
