using System;

namespace _project0_gt
{
    class Program// properties
    {
        static string name = "";
        static void Main(string[] args)
        {
            Employee emp = new Employee(); //default constructor
            //emp.firstName = "Carol";
            //emp.lastName = "Baxtor";
            //emp.ssn = "asdf32";
            //emp.age = 22;
            //emp.salary = 2000000;
            //emp.human = false; cannot be done since it is changing a const variable
            //emp.GetDetails();

            // Calling parameterized Constructor with named parameters

            //Employee emp2 = new Employee("palmer", "calgoris", "asdf", 27, 250.00M);

            //Employee emp3 = new Employee(lastName:"calgoris", firstName: "Trini", ssn:"asdf234", age:29);
            Console.Write($"The salary is {emp._Salary}");
        }
    }
}