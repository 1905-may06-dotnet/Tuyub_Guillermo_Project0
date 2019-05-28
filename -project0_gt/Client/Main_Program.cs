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
            AccountLogic    uu = new AccountLogic();
            ResLocationLogic rl = new ResLocationLogic();
            PizzaLogic      pl = new PizzaLogic();
            userChoicesLogic ucl = new userChoicesLogic();

            #region textOfProcess
            //sign or register OR ADMIN
            //sign in:check db for user/pw & store userID
            //register:create user in db (improvement: check if username alerady exists)
            //choose location(used to deduce which inventory to pull/push from)
            //sets locaitonid
            //(ought to check for time last purchased from this location)
            //order pizza
            //decide crust/size ... assumption: all pizza places have the same size/crust... ideally pull from separate inventory
            //toppings shown pulled from specific inventory
            //set inventory into program and cycle through ingredients in local storage
            //show price/current make of pizza using local storage.
            //price is done by calculating LOCAL PIZZA ... ADD LOCAL pizza price to LOCAL STORAGE
            //RESET LOCAL pizza and only send LOCAL STORAGE to DATABASE once order is fulfilled.
            //

            //order more pizza Y/N
            //Reset cost,ingredients list, and toppings0-4.
            //go through order pizza again.
            //LOCAL -> LOCAL STORAGE -> DATABASE
            //LOCAL only holds one pizza TYPE at a time
            //LOCAL STORAGE holds multiple pizza TYPES at a time
            //confirm order: Y/N
            //this is where LOCAL STORAGE is sent to DATABASE
            //When sending LOCAL STORAGE to DATABASE, multiple "uploads" required with the same ORDERID.
            //LOCAL STORAGE includes: toppings0-4, crust,size,count,cost,orderid
            //loop back to sign/register/admin .. order choose location?


            //ADMIN:
            //admin is just displaying already existing information in the database
            //functionality could use improvement, but working nonetheless.
            #endregion

            userInput();
            void userInput(){
                //sign or register should return state 1 2 or 3

                
                //T: sign in || F: admin || recursive on register

                pl.setusernameid(uu.SignOrRegister());
                pl.setlocationFID(rl.chooseRestaurant());
                pl.setThisInventory();
                pl.setThisIngredientsList();
                pl.setThisIngredientsString();
                pl.initializeCost();

                recursionToInput();
                }

            void recursionToInput()
            {
                bool b = ucl.choices(pl.currentingredients);
                ucl.checkcustomize(pl.currentingredients, b);
                recursionToInput();
            }
        }
    }
}
