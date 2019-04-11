using Microsoft.AspNetCore.Mvc;

namespace LombardAPI.Controllers
{
    public class TransactionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}