using ENTITIES.Models;
using Microsoft.AspNetCore.Mvc;
using ProxyServer;

namespace WebApplicationOrders.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductProxy _proxy;

        public ProductsController()
        {
            this._proxy = new ProductProxy();
        }
        // GET: Product
        public async Task<IActionResult> Index()
        {
            var products = await _proxy.GetAllAsync();
            return View(products);
        }

        // Create
        // GET: Product 
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductCreate
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,ProductName,SupplierId,UnitPrice,Package,IsDiscontinued")] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _proxy.CreateAsync(product);
                    if (result == null)
                    {
                        return RedirectToAction("Error", new { message = "El Producto ya existe" });
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // throw;
                    return RedirectToAction("Error", new { message = ex.Message });
                }
            }
            return View(product);
        }

        //Edit

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int Id)
        {
            var product = await _proxy.GetByIdAsync(Id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,SupplierId,UnitPrice,Package,IsDiscontinued")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _proxy.UpdateAsync(id, product);
                    if (!result)
                    {
                        return RedirectToAction("Error", new { message = "No se puede realizar la edición porque hay duplicidad de nombre con otro producto." });
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", new { message = ex.Message });
                }
            }

            return View(product);
        }

        // Details

        // GET: /Product/Details/5
        public async Task<IActionResult> Details(int Id)
        {
            var product = await _proxy.GetByIdAsync(Id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // Delete
        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _proxy.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _proxy.DeleteAsync(id);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

                return RedirectToAction("Error", new { message = ex.Message });
            }
        }

        // Error
        public IActionResult Error(string message)
        {
            ViewBag.ErrorMessage = message;
            return View();
        }



    }
}
