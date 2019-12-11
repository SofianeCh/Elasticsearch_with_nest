using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Elasticsearch
{
    class Linq
    {
        public Linq(IReadOnlyCollection<Accounts> querD, IReadOnlyCollection<Accounts> data1)
        {
            //Filter and keep only those who are 21y and sort list by firstname

            IEnumerable<Accounts> linqT = from cust in querD
                                          where cust.age == 21
                                          orderby cust.firstname ascending
                                          select cust;

            // filter your list by states and put them in groups 

            var linqT2 = from cust in querD
                         group cust by cust.state;
            int count = 0;
            foreach (var item in linqT)
            {
                Console.WriteLine("-> " + item.firstname + " " + item.lastname + " " + item.age + " " + item.gender);
                count++;
            }
            Console.WriteLine($"there is {count} after the linq");
            Console.WriteLine();
            foreach (var item in linqT2)
            {
                Console.WriteLine(item.Key);
                foreach (var i in item)
                {
                    Console.WriteLine("---> " + $"{i.firstname}" + "\t\t" + $"{i.lastname}" + "\t\t" + $"{ i.age}" + "\t" + $"{ i.gender}" + "\t" + $"{ i.state}");
                }
            }

            var linqjoin = (from male in querD
                            where male.age == 21
                            select male.firstname)
                           .Concat(from female in data1
                                   where female.age == 21
                                   select female.firstname);
            foreach (var item in linqjoin)
            {
                Console.WriteLine(item);
            }

        }
    }

}
