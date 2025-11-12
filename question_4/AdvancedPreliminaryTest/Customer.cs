using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedPreliminaryTest
{
    public class Customer : BaseObject
    {

        public Customer(string name)
        {
            this.Name = name;
        }

        public string Name
        {
            get;
            set;
        }

    }
}
