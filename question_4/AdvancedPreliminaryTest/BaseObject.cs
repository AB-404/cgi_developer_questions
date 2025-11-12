using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedPreliminaryTest
{
    public class BaseObject
    {

        private static int CurrentId = 0;

        public BaseObject()
        {
            this.Id = ++CurrentId;
        }

        public int Id
        {
            get;
            protected set;
        }

    }
}
