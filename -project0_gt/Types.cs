using System;

namespace _project0_gt
{
    class Employee // properties
    {
        //variables camelCase
        string firstName, lastName, ssn;
        int age;
        decimal salary;

        //methods = Specifications + definition(body)
        public void GetDetails(){
            Console.WriteLine("Name {0} {1} has Social Security - {2}. The Salary is {3}", firstName, lastName, ssn, salary);
        }
    }
}
