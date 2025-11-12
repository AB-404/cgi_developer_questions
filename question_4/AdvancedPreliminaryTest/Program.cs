using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedPreliminaryTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var startTime = DateTime.Now;

            foreach (var customer in DataStore.Instance.Customers())
            {
                var products = DataStore.Instance.GetProductsOrderedByCustomer(customer);
                if (products.Any())
                {
                    Console.WriteLine("Customer {0} has ordered the following products: {1}", customer.Name, String.Join(", ", products.Select(p => p.Name).ToArray()));
                }
                else
                {
                    Console.WriteLine("{0} has not ordered any products", customer.Name);
                }
            }

            Console.WriteLine("Finished, time taken: {0} ms.\r\nPress any key to exit.", ((DateTime.Now - startTime).Ticks / TimeSpan.TicksPerMillisecond));
            Console.ReadLine();
        }
    }
}
