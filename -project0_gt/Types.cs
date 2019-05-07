using System;

namespace _project0_gt
{
    class Employee // properties
    {

        //internal const bool ishuman = true; //val cannot be changed
        //variables camelCase
        //declaration of variables

        internal  string firstName, lastName, ssn;
        internal int age;
        private decimal salary;

        //properties: Smart fields to access private variables outside the class
        public decimal _Salary{
            get{
                return salary;
            }
            set{
                salary = value;
            }
        }
        //methods = Specifications + definition(body)
        public void GetDetails(){
            Console.WriteLine($"Name {firstName} {lastName} has Social Security - {ssn}. The Salary is {salary}");
        }

        //constructors in C# : Special methods .. used to intialize memory to the object
        //parameterless constructor
        public Employee(){ //constructor should have name as the class
            firstName = "Cameron";
            lastName = "Coley";
            ssn = "asdf32";
            age = 22;
            salary = 2000000.00M;
            Console.WriteLine($"Name {firstName} {lastName} has Social Security - {ssn}. The Salary is {salary}");

        }

        //parameterized constructors
        public Employee(string firstName,string lastName,string ssn, int age, decimal salary=5000.50M)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.ssn = ssn;
            this.age = age;
            this.salary = salary;
            Console.WriteLine($"Name {firstName} {lastName} has Social Security - {ssn}. The Salary is {salary}");

        }
    }
}
