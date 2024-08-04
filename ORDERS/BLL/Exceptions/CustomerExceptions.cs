using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class CustomerExceptions : Exception
    {
        // You can add more static methods here to throw other customer-related exceptions
        private CustomerExceptions(string message) : base (message)
        {
            // Optional: Add constructor logic for logging or custom error handling
        }

        public static void ThrowCustomerAlreadyExistsException(string firstName, string lastName)
        {
            throw new CustomerExceptions($"A client with the name already exists {firstName} {lastName}.");
        }

        public static void ThrowInvalidCustomerDataException(string message) 
        { 
            throw new CustomerExceptions(message);
        }

        public static void ThrowInvalidCustomerIdException(int id) 
        {
            throw new CustomerExceptions($"A client with the Id {id} not exists .");
        }
    }
}
