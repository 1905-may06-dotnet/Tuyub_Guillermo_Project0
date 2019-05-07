using System;

namespace _project0_gt
{
    class Employee // properties
    {
        //variables camelCase
        internal  string firstName, lastName, ssn;
        internal int age;
        internal decimal salary;

        //methods = Specifications + definition(body)
        public void GetDetails(){
            Console.WriteLine($"Name {firstName} {lastName} has Social Security - {ssn}. The Salary is {salary}");
        }
    }
}
