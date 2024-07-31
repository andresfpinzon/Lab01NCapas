using Microsoft.AspNetCore.Mvc;
using ProxyServer;
using ENTITIES.Models;

namespace WebApplicationOrders.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomerProxy _proxy;

        public CustomersController()
        {
            this._proxy = new CustomerProxy();
        }
        // GET: Customer 
        public async Task<IActionResult> Index()
        {
            var customers = await _proxy.GetAllAsync();
            return View(customers);
        }

        // Create
        // GET: Customer 
        public IActionResult Create() 
        { 
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,City,Country,Phone")] Customer customer)
        {
            if (ModelState.IsValid) {
                try
                {
                    var result = await _proxy.CreateAsync(customer);
                    if (result == null) 
                    { 
                        return RedirectToAction("Error", new { message = "El cliente con el mismo nombre y apellido ya existe"});
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // throw;
                    return RedirectToAction("Error", new { message = ex.Message });
                }
            }
            return View(customer);
        }

    }
}
