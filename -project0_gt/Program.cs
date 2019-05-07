using System;

namespace _project0_gt
{
    class Program// properties
    {
        static string name = "";
        static void Main(string[] args)
        {
            Employee emp = new Employee();
            emp.firstName = "Carol";
            emp.lastName = "Baxtor";
            emp.ssn = "asdf32";
            emp.age = 22;
            emp.salary = 2000000;

            emp.GetDetails();
        }
    }
}