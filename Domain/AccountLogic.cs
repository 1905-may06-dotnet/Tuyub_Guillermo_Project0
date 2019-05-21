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

        pizzadb_gtContext dbui = new pizzadb_gtContext();
        UserInfo ui = new UserInfo();

        private UserInfo initializeInfo(string name) //requires username first
        {

            UserInfo x = dbui.UserInfo.Where<UserInfo>(u => u.UserName == name).FirstOrDefault<UserInfo>();
            return x;


        }

        internal string UserName=null;
        private string getUName()
        {
            return ui.UserName;
        }
        private void setUName(string str)
        {
            ui.UserName = str;
        }

        private string FirstName;
        private void setFName(string str)
        {
            ui.FirstName = str;
        }

        private string LastName;
        private void setLName(string str)
        {
            ui.LastName = str;
        }

        private int PhoneNumber;
        private void setPhone(int value)
        {
            ui.PhoneNumber = value;
        }

        private string Password;
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
           // Console.WriteLine(input);
            if (input.Equals("r"))
            {
                //regUser();
                Console.WriteLine("Create Username: ");
                setUName(Console.ReadLine()); //upload username to database ... still needs function created

                Console.WriteLine("Enter FirstName:");
                setFName(Console.ReadLine());
                Console.WriteLine("Enter LastName: "); // upload to db, rq func
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

                RestaurantLogic.DbInstance.Instance.UserInfo.Add(ui);
                RestaurantLogic.DbInstance.Instance.SaveChanges();

                Console.WriteLine("type  S to sign in or R to register another acc");
                SignOrRegister(Console.ReadLine().ToLower());

                //upload info to db
            }
            else if (input.Equals("s"))
            {

                Console.WriteLine("Insert username: ");


                input = Console.ReadLine();

                ui = initializeInfo(input);
                if (ui == null)
                {
                    Console.WriteLine("user not in database");
                    Console.WriteLine("S: sign in | R: register");
                    SignOrRegister(Console.ReadLine().ToLower());
                }
                else if (ui.UserName == input)
                {
                    //setUName(input);
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
            else
            {
                Console.WriteLine("please type in S to sign in or R to register");
                SignOrRegister(Console.ReadLine().ToLower());
            }
        }

    }

}
