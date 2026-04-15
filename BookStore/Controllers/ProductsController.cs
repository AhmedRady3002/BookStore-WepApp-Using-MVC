using BookStore.ModelView;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using BookStore;
using Microsoft.AspNetCore.Authorization;
using BookStore.Models;
using Microsoft.EntityFrameworkCore; // نفس namespace بتاع SessionExtensions


namespace BookStore.Controllers
{
	public class ProductsController : Controller
	{
       
        private readonly IBookRepository bookRepository;
        private readonly IOrderdetailsRepository orderdetailsRepository;
        private readonly IOrederRepository orederRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly BookStoreContext _context;

        public ProductsController
			(IBookRepository IbookRepository,
			IOrderdetailsRepository orderdetailsRepository,
			IOrederRepository orederRepository,
			ICustomerRepository customerRepository, BookStoreContext context)
        {
            
            bookRepository = IbookRepository;
            this.orderdetailsRepository = orderdetailsRepository;
            this.orederRepository = orederRepository;
            this.customerRepository = customerRepository;
            _context = context;
        }
        public IActionResult ShowProduct()
		{
            var books = bookRepository.GetBooksWithCategoryAndAuthorAsTuple();
           List< ShowProductsViewModel> productsViewModel = new List<ShowProductsViewModel>();
            foreach (var item in books)
            {
                ShowProductsViewModel showProductsViewModel =new ShowProductsViewModel
				{
					bookid = item.bookid,
					bookname = item.bookname,
					price = item.price,
					Quantity = item.Quantity,
					Author_Name = item.Author_Name,
					Category_Name = item.Category_Name,
				     Date = item.Date,
				    ImageURL = item.ImageURL,
				};
				productsViewModel.Add(showProductsViewModel);
            }
            return View("ShowProducts",productsViewModel);
		}
		public IActionResult Search(string Name)
		{
			var books = bookRepository.GetBookByName(Name);
			if (books == null)
			{
				return RedirectToAction("ShowProduct");
			}
			return View("ShowProducts",books);
		}
		public IActionResult BookDetails(int id)
        {
           var book= bookRepository.GetBooksWithCategoryAndAuthorAsTupleByBookID(id);
           BookDEtailsViewModel bookDEtailsViewModel = new BookDEtailsViewModel(); 
            bookDEtailsViewModel.bookid=book.Value.bookid;
            bookDEtailsViewModel.bookname=book.Value.bookname;
            bookDEtailsViewModel.Author_Name = book.Value.Author_Name;
            bookDEtailsViewModel.Category_Name = book.Value.Category_Name;
            bookDEtailsViewModel.description=book.Value.description;
            bookDEtailsViewModel.Date=book.Value.Date;
            bookDEtailsViewModel.ImageURL= book.Value.ImageURL;
            bookDEtailsViewModel.price=book.Value.price;
            bookDEtailsViewModel.Quantity = book.Value.Quantity;

           
            return View("BookDetails",bookDEtailsViewModel);
        }
		
		public IActionResult AddToCart(int QuantityRequired, int bookid)
		{
			var bookdb = bookRepository.GetBookByID(bookid);
			if (User.Identity.IsAuthenticated != true)
			{
                TempData["CreateUser"] = "Create User";
                return RedirectToAction("BookDetails", new { id = bookid });

            }
		
			else if (QuantityRequired > 0 && QuantityRequired<=bookdb.AvailableQuantity)
			{
				var book = bookRepository.GetBookByID(bookid);

				var cart = HttpContext.Session.GetObjectFromJson<List<ContentOfCart>>("Cart");

				if (cart == null)
					cart = new List<ContentOfCart>();

				var existingItem = cart.FirstOrDefault(x => x.bookId == bookid);

				if (existingItem != null)
				{
					existingItem.Quantity_Required += QuantityRequired;
					existingItem.TotalPrice = existingItem.Quantity_Required * existingItem.Price;
				}
				else
				{
					cart.Add(new ContentOfCart
					{
						bookId = bookid,
						BookName = book.Name,
						Price = book.Price,
						ImagURL = book.ImageURL,
						Quantity_Required = QuantityRequired,
						TotalPrice = book.Price * QuantityRequired,
					});
				}

				HttpContext.Session.SetObjectAsJson("Cart", cart);

				TempData["SuccessMessage"] = "تم إضافة المنتج للكارت بنجاح";
				return RedirectToAction("BookDetails", new { id = bookid });
			}

			TempData["QuantityNotAvailabe"] ="   الكميه  غير مناسبه  ";

			return RedirectToAction("BookDetails", new { id = bookid });
		}


		[Authorize]
		public IActionResult SeeCart()
		{
			var cart = HttpContext.Session.GetObjectFromJson<List<ContentOfCart>>("Cart");

			if (cart == null)
				cart = new List<ContentOfCart>();

			return View("Cart", cart);
		}
        public IActionResult DeleteFromCart(int id)
        {
			var cart = HttpContext.Session
						 .GetObjectFromJson<List<ContentOfCart>>("Cart");

					   if (cart==null)
				cart = new List<ContentOfCart>();
			

            var item = cart.FirstOrDefault(c => c.bookId == id);

            if (item != null)
            {
                cart.Remove(item);
               HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            return View("Cart", cart);
        }


        public IActionResult  CheckOut()
		{
            var cart = HttpContext.Session.GetObjectFromJson<List<ContentOfCart>>("Cart");

            if (cart == null)
                cart = new List<ContentOfCart>();
			ViewBag.Cart = cart;
            return View ("CheckOut");
		}

        public ActionResult SaveCheckOut(CustomerData customerData)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<ContentOfCart>>("Cart");

            if (!ModelState.IsValid || cart == null || !cart.Any())
            {
                ModelState.AddModelError("", "ضيف منتج للكارت");
                ViewBag.Cart = cart ?? new List<ContentOfCart>();
                return View("CheckOut", customerData);
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    double totalPrice = 0;

                    
                    var customer = customerRepository.GetByPhone(customerData.Phone);
                    if (customer == null)
                    {
                        customer = new Customer
                        {
                            CustomerName = customerData.Name,
                            CustomerPhone = customerData.Phone,
                            Governate = customerData.Governate,
                            Markaz = customerData.Markaz,
                            Village = customerData.Village
                        };
                        customerRepository.Add(customer);
                        _context.SaveChanges();
                    }

                   
                    foreach (var item in cart)
                        totalPrice += item.TotalPrice;

                    Order order = new Order
                    {
                        CustomerID = customer.CustomerID,
                        TotalPrice = totalPrice,
                        OrderStatus = OrderStatus.Processing
                    };
                    orederRepository.Add(order);
                    _context.SaveChanges();

                    foreach (var item in cart)
                    {
                        var book = bookRepository.GetBookByID(item.bookId);

                   
                        if (book.AvailableQuantity < item.Quantity_Required)
                            throw new Exception($"الكمية المطلوبة من كتاب '{book.Name}' غير متوفرة");

                        book.AvailableQuantity -= item.Quantity_Required;

                        
                        OrderDetails details = new OrderDetails
                        {
                            OrderID = order.OrderID,
                            BookID = book.Id,
                            Price = item.Price,
                            QuantitySold = item.Quantity_Required
                        };
                        orderdetailsRepository.Add(details);
                    }

                    try
                    {
                        _context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        transaction.Rollback();
                       ModelState.AddModelError("", "حدث خطأ: أحد المستخدمين اشترى نفس الكتاب قبل أن تكمل الطلب. الكمية غير كافية الآن.");
                        ViewBag.Cart = cart;
                        return View("CheckOut", customerData);
                    }

                   
                    transaction.Commit();
                    
                    TempData["OrderID"] = order.OrderID;
                    return RedirectToAction("OrderConfirmation", new { orderId = order.OrderID });

                   

                    
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    ModelState.AddModelError("", $"حدث خطأ أثناء تنفيذ الطلب: {ex.Message}");
                    ViewBag.Cart = cart;
                    return View("CheckOut", customerData);
                }

            }
        }

        public IActionResult OrderConfirmation() 
		{
            var cart = HttpContext.Session.GetObjectFromJson<List<ContentOfCart>>("Cart");
			if (cart == null)
				cart = new List<ContentOfCart>();
			var orderID = TempData["OrderID"] != null ? (int)TempData["OrderID"] : 0;

			ViewBag.OrderID = orderID;
			return View("OrderConfirmation",cart);
		}

		public IActionResult ProcessOfBilling(int bookid, int QuantityRequired) 
		{
		var book=bookRepository.GetBookByID(bookid);
			if (QuantityRequired == 0 ||QuantityRequired>book.AvailableQuantity)
			{
				return RedirectToAction("BookDetails", new { id = bookid });
			}
			else 
			{
				ViewBag.bookid = bookid;
				ViewData["QuantityRequired"] = QuantityRequired;
				return View("CheckOutWitoutCart");
			}

		}
        public IActionResult SaveProcessOfBilling(CustomerData customerData, int QuantityRequired, int bookid)
        {
            if (!ModelState.IsValid)
                return View("CheckOutWitoutCart", customerData);

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                
                var customer = customerRepository.GetByPhone(customerData.Phone);
                if (customer == null)
                {
                    customer = new Customer
                    {
                        CustomerName = customerData.Name,
                        CustomerPhone = customerData.Phone,
                        Governate = customerData.Governate,
                        Markaz = customerData.Markaz,
                        Village = customerData.Village
                    };
                    customerRepository.Add(customer);
                    _context.SaveChanges();
                }

                
                var book = bookRepository.GetBookByID(bookid);

                
                if (book.AvailableQuantity < QuantityRequired)
                {
                    ModelState.AddModelError("", "الكمية المطلوبة غير متوفرة.");
                    return View("CheckOutWitoutCart", customerData);
                }


               
                var order = new Order
                {
                    TotalPrice = book.Price * QuantityRequired,
                    OrderStatus = OrderStatus.Processing,
                    CustomerID = customer.CustomerID
                };
                orederRepository.Add(order);
                _context.SaveChanges();

               
                var details = new OrderDetails
                {
                    OrderID = order.OrderID,
                    BookID = book.Id,
                    Price = book.Price,
                    QuantitySold = QuantityRequired
                };
                orderdetailsRepository.Add(details);

               
                book.AvailableQuantity -= QuantityRequired;

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    transaction.Rollback();
                    ModelState.AddModelError("", "حدث خطأ: أحد المستخدمين اشترى نفس الكتاب قبل أن تكمل الطلب. الكمية غير كافية الآن.");
                    return View("CheckOutWitoutCart", customerData);
                }

               
                transaction.Commit();

                return RedirectToAction("OrderConfirmationWithoutCart", new { orderId = order.OrderID });
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                ModelState.AddModelError("", $"حدث خطأ أثناء تنفيذ الطلب: {ex.Message}");
                return View("CheckOutWitoutCart", customerData);
            }
        }
        public IActionResult OrderConfirmationWithoutCart(int orderId)
        {
            var order = orederRepository.GetOderById(orderId);
            if (order == null)
                return NotFound();

            var orderDetails = orderdetailsRepository.GetOderdetailsByOrderId(orderId);
            if (orderDetails == null)
                return NotFound();

            var book = bookRepository.GetBookByID(orderDetails.BookID);
            if (book == null)
                return NotFound();

            var vm = new OrderConfirmationWithoutCartViewModel
            {
                bookName = book.Name,
                orderId = order.OrderID,
                quantity = orderDetails.QuantitySold,
                totalprice = order.TotalPrice,
                imageUrl = book.ImageURL
            };
           
            return View(vm);
        }

		








    }
}
