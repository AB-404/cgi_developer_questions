# CGI Developer Questions
Each solution to the question is located in the corresponding folder.

## Question 1 Solution
<img width="1010" height="554" alt="image" src="https://github.com/user-attachments/assets/5d9e4b7e-47a5-43cd-925b-d8fbccf29eda" />

## Question 2 Solution
```C#
public static List<PipesList> RemoveTheSmallPipes(List<PipesList> lstPipeTypes)
{
  foreach (var pipeList in lstPipeTypes)
  {
      //Remove each pipe with a length less than 20
      pipeList.Pipes.RemoveAll(pipe => pipe.length < 20);
  }

  return lstPipeTypes;

}
```

## Question 3 Solution
### Part 1
```HTML
<html>

<head>
<style type="text/css">

body {
  background-color: white;
}

table.outer {
  background-color: yellow;
}

table.inner {
  background-color: green;
  border: 3px solid red;
  color: black;
}

h3 {
  color: white;
}

</style>
</head>

<body>

<table class=outer><tr><td colspan=3>Question E3</td></tr><td colspan=3><table class=inner><tr><td colspan=3><h3>How much does fruit weigh?</h3></td></tr>
 <tr><td rowspan=6>&nbsp;&nbsp;</td><td>Apples</td><td>300g</td></tr><tr><td>Oranges</td><td>350g</td></tr><tr><td>Bananas</td><td>250g</td></tr><tr><td>Pears</td><td>150g</td></tr><tr><td>Pineapples</td><td>870g</td></tr><tr><td>Kiwis</td><td>60g</td>
 </tr></table></td></tr></table>
</body>

</html>
```

### Part 2
```HTML
<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<style>

  .container {
    width: 80%;
    margin: 20px auto;
  }

  .header, .footer {
    background-color: #bbb;
    border: 1px solid black;
    height: 80px;
    margin-bottom: 10px;
  }

  .middle {
    display: flex;
    justify-content: space-between;
    margin-bottom: 10px;
  }

  .column {
    background-color: #bbb;
    border: 1px solid black;
    width: 32%;
    height: 600px;
  }
</style>
</head>

<body>

<div class="container">
  <div class="header"></div>

  <div class="middle">
    <div class="column"></div>
    <div class="column"></div>
    <div class="column"></div>
  </div>

  <div class="footer"></div>
</div>

</body>
</html>
```

## Question 4
```C#
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
```
