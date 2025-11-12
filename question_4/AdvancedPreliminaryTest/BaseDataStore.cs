using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdvancedPreliminaryTest
{
    public class BaseDataStore
    {

        /// <summary>
        /// CANDIDATE
        /// This is an artificial delay intended to mimic the delay when hitting a slow database
        /// </summary>
        private void Delay()
        {
            System.Threading.Thread.Sleep(1000);
        }

        private static DataStore _Instance;
        public static DataStore Instance
        {
            get
            {
                return _Instance;
            }
        }

        static BaseDataStore()
        {
            _Instance = new DataStore();
            _Instance._Customers = new List<Customer>()
            {
                new Customer("Bob Jones"),
                new Customer("Terry Smith"),
                new Customer("Joe Strummer")
            };

            _Instance._Products = new List<Product>()
            {
                new Product("Hat"),
                new Product("Shoes"),
                new Product("Bag")
            };

            _Instance._Orders = new List<Order>()
            {
                new Order(_Instance._Customers[0]),
                new Order(_Instance._Customers[1]),
                new Order(_Instance._Customers[1])
            };

            _Instance._OrderItems = new List<OrderItem>()
            {
                new OrderItem(_Instance._Orders[0], _Instance._Products[0], 1),
                new OrderItem(_Instance._Orders[0], _Instance._Products[1], 1),
                new OrderItem(_Instance._Orders[1], _Instance._Products[1], 2),
                new OrderItem(_Instance._Orders[1], _Instance._Products[2], 1),
                new OrderItem(_Instance._Orders[2], _Instance._Products[0], 1)
            };
        }

        public virtual void Save(BaseObject databaseObject)
        {
            Delay();
        }

        private List<Customer> _Customers = null;
        public List<Customer> Customers()
        {
            Delay();
            return _Customers;
        }

        private List<Product> _Products = null;
        protected List<Product> Products()
        {
            Delay();
            return _Products;
        }

        private List<Order> _Orders = null;
        protected List<Order> Orders()
        {
            Delay();
            return _Orders;
        }

        private List<OrderItem> _OrderItems = null;
        protected List<OrderItem> OrderItems()
        {
            Delay();
            return _OrderItems;
        }

        public IEnumerable<Order> GetOrdersByCustomerId(int customerId)
        {
            return this.Orders().Where(o => o.CustomerId == customerId);
        }

        public IEnumerable<OrderItem> GetOrderItemsByOrderIdAndProductId(int orderId, int productId)
        {
            return this.OrderItems()
                .Where(oi => oi.OrderId == orderId && oi.ProductId == productId);
        }

        public Product GetProductById(int productId)
        {
            return this.Products().First(p => p.Id == productId);
        }

        public virtual IEnumerable<Product> GetProductsOrderedByCustomer(Customer customer)
        {
            return this.Products().Where(p => this.GetOrderItemsByCustomerIdAndProductId(customer.Id, p.Id).Any());
        }

        public virtual Order GetOrderById(int orderId)
        {
            return this.Orders().First(o => o.Id == orderId);
        }

        public virtual IEnumerable<OrderItem> GetOrderItemsByCustomerIdAndProductId(int customerId, int productId)
        {
            return this.GetOrderItemsByCustomerId(customerId).Where(oi => oi.ProductId == productId);
        }

        public virtual IEnumerable<OrderItem> GetOrderItemsByCustomerId(int customerId)
        {
            return this.OrderItems().Where(oi => this.GetOrderById(oi.OrderId).CustomerId == customerId);
        }

    }
}
