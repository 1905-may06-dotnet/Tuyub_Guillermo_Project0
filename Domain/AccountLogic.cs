using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
//using System.Data.entity;
//using System.Data.infrastructure;
//using System.Data.Core.Objects;
using Data.Models;

namespace Domain
{
    public class User
    {

        static int i = 1;
        UserInfo ui = new UserInfo();
        RestaurantLogic rel = new RestaurantLogic();
        public UserInfo initializeInfo(string name) //requires username first
        {

            UserInfo x = Crud.DbInstance.Instance.UserInfo.Where<UserInfo>(u => u.UserName == name).FirstOrDefault<UserInfo>();
            return x;


        }
        private void addUserInfoToQ()
        {
            Crud.DbInstance.Instance.Add(ui);
        }
        private void commitToDb()
        {
            Crud.DbInstance.Instance.SaveChanges();
        }
        public string getUName()
        {
            return ui.UserName;
        }
        public int getUID()
        {
            return ui.UserId;
        }
        private void setUID(int num)
        {
            ui.UserId = num;
        }
        private void setUName(string str)
        {
            ui.UserName = str;
        }
        private void setFName(string str)
        {
            ui.FirstName = str;
        }
        private void setLName(string str)
        {
            ui.LastName = str;
        }
        private void setPhone(int value)
        {
            ui.PhoneNumber = value;
        }
        private void setPW(string pw)
        {
            ui.Password = pw;
        }
        private void checkPW(string pw, string check)
        {            
            if (pw.Equals(check)) {
                Console.WriteLine("Password Accepted");
            }
            else if (i <= 3) {
                Console.WriteLine($"(Attempt {i} | Password not Accepted, try again:");
                i++;
                pw = Console.ReadLine();
                checkPW(pw, check);
            }
            else
            {

                Console.WriteLine("Too many password attempts: ");
                Environment.Exit(0);
            }
            
        }




        public void SignOrRegister(string input)
        {
            if (input.Equals("r"))
            {
                
                Console.WriteLine("Create Username: ");
                setUName(Console.ReadLine());

                Console.WriteLine("Enter FirstName:");
                setFName(Console.ReadLine());

                Console.WriteLine("Enter LastName: ");
                setLName(Console.ReadLine());
                Console.WriteLine("Enter Phone number: ");
                try
                {
                    setPhone(Int32.Parse(Console.ReadLine()));
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Unable to parse '{input}'");
                }
                catch (Exception)
                {
                    Console.WriteLine("Error unknown, not a formatexception");
                }

                Console.WriteLine("Create password: ");
                setPW(Console.ReadLine());

                addUserInfoToQ();
                commitToDb();

                Console.WriteLine("type  S to sign in or R to register another acc");
                SignOrRegister(Console.ReadLine().ToLower());

                //upload info to db
            }
            else if (input.Equals("s"))
            {

                Console.WriteLine("Insert username: ");


                input = Console.ReadLine();

                //ui = initializeInfo(input);
                if (initializeInfo(input) == null)
                {
                    Console.WriteLine("Username not in database");
                    Console.WriteLine("S: sign in | R: register");
                    SignOrRegister(Console.ReadLine().ToLower());
                }
                else if (initializeInfo(input).UserName == input)
                {
                    ui = initializeInfo(input);
                    setUID(ui.UserId);
                    Console.WriteLine("User matched");
                    Console.WriteLine("Insert password: ");
                    checkPW(Console.ReadLine(), ui.Password);// check if password matches database/username if y:continue, if n:prompt user for password again //forgot password

                }
                else
                {
                    Console.WriteLine("username not in database");
                    Console.WriteLine("please type in S to sign in or R to register");
                    SignOrRegister(Console.ReadLine().ToLower());
                }

            }
            else if (input.Equals("admin"))
            {

                PizzaLogic pl = new PizzaLogic();
                List<Orderpizza> oplist = new List<Orderpizza>();
                RestaurantLogic rl = new RestaurantLogic();
                ResLocation res = new ResLocation();
                List<IndivPizza> iplist = new List<IndivPizza>();
                List<Inventory> invlist = new List<Inventory>();
                List<Ingredients> inglist = new List<Ingredients>();
                List<UserInfo> userlist = new List<UserInfo>();

                Console.WriteLine("Hello admin, Choose Location: ");
                rel.showLocation();
                res = rel.chooseRestaurant(Console.ReadLine());
                Console.WriteLine($"\t\t {res.ResName} chosen");

                oplist = Crud.DbInstance.Instance.Orderpizza.Where<Orderpizza>(x => x.LocationFid == res.LocationId).ToList<Orderpizza>();
                iplist = Crud.DbInstance.Instance.IndivPizza.ToList<IndivPizza>();
                invlist = Crud.DbInstance.Instance.Inventory.Where<Inventory>(x => x.Resfid == res.LocationId).ToList<Inventory>();
                inglist = Crud.DbInstance.Instance.Ingredients.ToList<Ingredients>();
                userlist = Crud.DbInstance.Instance.UserInfo.ToList<UserInfo>();
                choose();

                void choose()
                {
                    Console.WriteLine("Options:\n1)orders\n2)sales\n3)inventory\n4)users");

                    int num = setnum();
                    int setnum(){
                    if (Int32.TryParse(Console.ReadLine(), out int number))
                    {
                        return number;
                    }
                    else
                    {
                        Console.WriteLine("insert num 1-3");
                        return setnum();
                    }
                        }

                    if (num == 1)
                    {
                        foreach (Orderpizza pizza in oplist) {
                            Console.WriteLine($"\norderid: {pizza.OrderId} userid: {pizza.UserFid} timecheck:{pizza.Timecheck}");
                        }
                    }
                    else if (num == 2)
                    {
                        foreach (IndivPizza ip in iplist)
                        {
                            foreach (Orderpizza op in oplist)
                            {
                                if(op.OrderId ==ip.OrderFid)
                                    Console.WriteLine($"pizzaid: {ip.PizzaId} price:{ip.Totalcost} in orderid: {ip.OrderFid}");
                            }
                        }
                        //get orderpizza + indiv_pizza from db with locationid
                    }
                    else if (num == 3)
                    {
                        foreach(Inventory inv in invlist)
                        {
                            Console.WriteLine($"{inglist[(int)inv.FkIngredient].Ingredient} stock: {inv.Stock}");
                        }
                    }
                    else if (num == 4)
                    {
                        foreach(UserInfo ui in userlist)
                        {
                            Console.WriteLine($"{ui.UserId} uname: {ui.UserName} fname: {ui.FirstName} lastname: {ui.LastName} phonenumber: {ui.PhoneNumber}");
                        }
                    }
                    else
                        Environment.Exit(0);
                    choose();
                }




            }
            else
            {
                Console.WriteLine("please type in S to sign in or R to register");
                SignOrRegister(Console.ReadLine().ToLower());
            }
        }

    }

}
