using BLL;
using ENTITIES.Models;
using Microsoft.AspNetCore.Mvc;
using SLC;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using BLL.Exceptions;

namespace Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase , ICustomerService
    {
        private readonly Customers _bll; // Dependency injection for better testability

        public CustomerController(Customers bll)
        {
            _bll = bll;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAll()
        {
            try
            {
                var result = await _bll.RetrieveAllAsync();
                return Ok(result); // Use IActionResult for more flexibility (200 OK)
            }
            catch (CustomerExceptions ex) // Catch specific business logic exceptions
            {
                return BadRequest(ex.Message); // Return 400 Bad Request with error message
            }
            catch (Exception ex) // Catch unhandled exceptions for logging and generic error response
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }


    }
}
