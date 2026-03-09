using LinQ_Task.Models;

namespace LinQ_Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new Models.ApplicationDbContext();
            // 1-List all customers' first and last names along with their email addresses. 

            var c1 = context.Customers.Select(c => new {
            
            c.FirstName,
            c.LastName,
            c.Email,

            }).ToList();
            
            //2- Retrieve all orders processed by a specific staff member (e.g., staff_id = 3). 
            var c2 = context.Orders.Where(
                o=>o.StaffId==3).ToList();
            
            //3- Get all products that belong to a category named "Mountain Bikes". 
            var c3 = context.Products.Where(
                p => p.Category.CategoryName == "Mountain Bikes").ToList();
            
            //4-Count the total number of orders per store. 
            var c4 = context.Orders
                .GroupBy(o => o.StoreId)
                .Select(o => new{
                o.Key,
                Count = o.Count()

                });
               
            
            //5- List all orders that have not been shipped yet (shipped_date is null). 
            var c5 = context.Orders.Where(o => o.ShippedDate == null);
                

           
            //6- Display each customer’s full name and the number of orders they have placed. 
            var c6 = context.Customers.Select(c => new
            {
                FullName = c.FirstName + " " + c.LastName,
                OrderCount = c.Orders.Count()

            }).ToList();


            
            //7- List all products that have never been ordered (not found in order_items). 
            var c7 = context.Products
                    .Where(p => !p.OrderItems.Any())
                    .Select(p => new
                    {
                        p.ProductId,
                        p.ProductName,
                        p.ListPrice
                    })
                    .ToList();
            // 8- Display products that have a quantity of less than 5 in any store stock. 
            var c8 = context.Products
                    .Where(p => p.Stocks.Any(s=>s.Quantity<5))
                    .Select(p => new
                    {
                        p.ProductId,
                        p.ProductName
                    })
                    .ToList();
            //9- Retrieve the first product from the products table. 
            var c9 = context.Products.FirstOrDefault();
            // 10- Retrieve all products from the products table with a certain model year. 
            var c10 = context.Products
                    .Where(p => p.ModelYear == 2018)
                    .Select(p => new
                    {
                    p.ProductId,
                    p.ProductName,
                    p.ModelYear,
                    })
                    .ToList();
            //11- Display each product with the number of times it was ordered. 
            var c11 = context.Products
                    .Select(p => new
                    {
                        p.ProductName,
                        OrderCount = p.OrderItems.Count()
                    })
                    .ToList();
            //12- Count the number of products in a specific category. 
            var c12 = context.Products
                    .Where(p => p.Category.CategoryName == "Mountain Bikes")
                    .Count();
            //13- Calculate the average list price of products. 
            var c13 = context.Products.Average(p=>p.ListPrice);
            //14- Retrieve a specific product from the products table by ID. 
            var product = context.Products
                    .SingleOrDefault(p => p.ProductId == 10);

            // 15- List all products that were ordered with a quantity greater than 3 in any order. 
            var c15 = context.Products
                .Where(p => p.OrderItems.Any(oi => oi.Quantity > 3))
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductName
                })
                .ToList();
            //16- Display each staff member’s name and how many orders they processed. 
            var c16 = context.Staffs.Select(s => new
            {
                s.FirstName,
                s.LastName,
                OrderCount = s.Orders.Count()
            }).ToList();
            //17- List active staff members only (active = true) along with their phone numbers. 
            var c17 = context.Staffs
                .Where(s => s.Active ==1)  // Active == true
                .Select(s => new
                {
                    FullName = s.FirstName + " " + s.LastName,
                    s.Phone
                })
                .ToList();
            // 18- List all products with their brand name and category name. 
            var c18 = context.Products
                .Select(p => new
                {
                    p.ProductName,
                    p.Brand.BrandName,
                    p.Category.CategoryName
                })
                .ToList();
            //19- Retrieve orders that are completed. 
            var c19 = context.Orders
                .Where(o => o.ShippedDate != null)
                .ToList();
            //20- List each product with the total quantity sold (sum of quantity from order_items). 
            var c20 = context.OrderItems
                .GroupBy(oi => oi.Product)
                .Select(g => new
                {
                    g.Key.ProductName,
                    TotalQuantitySold = g.Sum(oi => oi.Quantity)
                })
                .ToList();
        }
    }
}
