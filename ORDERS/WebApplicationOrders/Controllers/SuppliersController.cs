using ENTITIES.Models;
using Microsoft.AspNetCore.Mvc;
using ProxyServer;

namespace WebApplicationOrders.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly SupplierProxy _proxy;

        public SuppliersController()
        {
            this._proxy = new SupplierProxy();
        }
        // GET: Supplier
        public async Task<IActionResult> Index()
        {
            var suppliers = await _proxy.GetAllAsync();
            return View(suppliers);
        }

        // Create
        // GET: Supplier
        public IActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,CompanyName,ContactName,ContactTitle,City,Country,Phone,Fax")] Supplier suppliers)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _proxy.CreateAsync(suppliers);
                    if (result == null)
                    {
                        return RedirectToAction("Error", new { message = "un proveedor con la misma empresa ya existe" });
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // throw;
                    return RedirectToAction("Error", new { message = ex.Message });
                }
            }
            return View(suppliers);
        }

        //Edit

        // GET: Supplier/Edit/5
        public async Task<IActionResult> Edit(int Id)
        {
            var suppliers = await _proxy.GetByIdAsync(Id);
            if (suppliers == null)
            {
                return NotFound();
            }
            return View(suppliers);
        }


        // POST: Supplier/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyName,ContactName,ContactTitle,City,Country,Phone,Fax")] Supplier suppliers)
        {
            if (id != suppliers.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _proxy.UpdateAsync(id, suppliers);
                    if (!result)
                    {
                        return RedirectToAction("Error", new { message = "No se puede realizar la edición porque hay duplicidad de nombre con otra empresa." });
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", new { message = ex.Message });
                }
            }

            return View(suppliers);
        }

        // Details

        // GET: /Supplier/Details/5
        public async Task<IActionResult> Details(int Id)
        {
            var suppliers = await _proxy.GetByIdAsync(Id);
            if (suppliers == null)
            {
                return NotFound();
            }
            return View(suppliers);
        }


        // Delete
        // GET: Supplier/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var suppliers = await _proxy.GetByIdAsync(id);
            if (suppliers == null)
            {
                return NotFound();
            }
            return View(suppliers);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _proxy.DeleteAsync(id);
                if (!result)
                {
                    return RedirectToAction("Error", new { message = "No se puede eliminar el Proveedor porque tiene Productos asociados" });
                }
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
