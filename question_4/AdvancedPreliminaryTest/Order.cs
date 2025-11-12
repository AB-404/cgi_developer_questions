using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedPreliminaryTest
{
    public class Order : BaseObject
    {

        public Order(Customer customer)
        {
            this.CustomerId = customer.Id;
        }

        public int CustomerId
        {
            get;
            set;
        }

    }
}
