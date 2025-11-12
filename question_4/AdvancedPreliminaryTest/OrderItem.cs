using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedPreliminaryTest
{
    public class OrderItem : BaseObject
    {

        public OrderItem(Order order, Product product, int quantity)
        {
            this.OrderId = order.Id;
            this.ProductId = product.Id;
            this.Quantity = quantity;
        }

        public int OrderId
        {
            get;
            set;
        }

        public int ProductId
        {
            get;
            set;
        }

        public int Quantity
        {
            get;
            set;
        }

    }
}
