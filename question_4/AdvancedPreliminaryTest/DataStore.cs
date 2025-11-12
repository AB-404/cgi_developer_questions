using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AdvancedPreliminaryTest
{
    public class DataStore : BaseDataStore
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private List<Customer>  _cachedCustomers;
        private List<Product>   _cachedProducts;
        private List<Order>     _cachedOrders;
        private List<OrderItem> _cachedOrderItems;

        private void RefreshCache()
        {
            _lock.EnterWriteLock();
            try
            {
                _cachedCustomers  = base.Customers().ToList();
                _cachedProducts   = base.Products().ToList();
                _cachedOrders     = base.Orders().ToList();
                _cachedOrderItems = base.OrderItems().ToList();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public override void Save(BaseObject databaseObject)
        {
            base.Save(databaseObject);
            RefreshCache();
        }

        public override IEnumerable<Product> GetProductsOrderedByCustomer(Customer customer)
        {
            EnsureCacheInitialized();

            _lock.EnterReadLock();
            try
            {
                var customerOrders = _cachedOrders.Where(o => o.CustomerId == customer.Id).Select(o => o.Id).ToList();
                var productIds = _cachedOrderItems
                    .Where(oi => customerOrders.Contains(oi.OrderId))
                    .Select(oi => oi.ProductId)
                    .Distinct()
                    .ToList();

                return _cachedProducts.Where(p => productIds.Contains(p.Id)).ToList();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        private void EnsureCacheInitialized()
        {
            if (_cachedCustomers == null)
            {
                RefreshCache();
            }
        }

        public new List<Customer> Customers()
        {
            EnsureCacheInitialized();
            _lock.EnterReadLock();
            try
            {
                return _cachedCustomers.ToList();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }
}
