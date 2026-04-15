using BookStore.Models;
using BookStore.ModelView;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public partial class AdminController : Controller
    {
        public IActionResult GetOrders()
        {
            var orders = context.Orders
       .Include(o => o.Customer)
       .Include(o => o.OrderDetails)
           .ThenInclude(od => od.Book)
       .SelectMany(o => o.OrderDetails.Select(od => new OrderAdminVM
       {
           OrderID = o.OrderID,
           OrderDate = o.OrederDate,
           OrderStatus = o.OrderStatus.ToString(),
           CustomerName = o.Customer.CustomerName,
           CustomerPhone = o.Customer.CustomerPhone,
           Governate = o.Customer.Governate,
           Markaz = o.Customer.Markaz,
           Village = o.Customer.Village,
           BookName = od.Book.Name,
           QuantitySold = od.QuantitySold,
           TotalPrice = o.TotalPrice
       }))
       .ToList();

            return View("GetOrders",orders);
        }


        public IActionResult EditOrder(int id) 
        {
          var order= orederRepository.GetOderById(id);
            if (order != null)
            {
                return View("EditOrder",order);
            }
            return RedirectToAction("GetOrders");

        }
        public IActionResult SaveOrderEdite(int OrderID,OrderStatus OrderStatus) 
        {
            if (ModelState.IsValid)
            {
                var orderdb = orederRepository.GetOderById(OrderID);
                orderdb.OrderStatus = OrderStatus;
                orederRepository.Save();
                return RedirectToAction("GetOrders");
            }
            return RedirectToAction("EditOrder", new { OrderID });
        }



    }
}
