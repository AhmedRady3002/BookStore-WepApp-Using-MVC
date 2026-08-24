using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View("Chat");
        }
    }
}
