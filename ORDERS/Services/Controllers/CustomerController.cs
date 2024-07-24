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

    }
}
