using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    internal class CustomerExceptions : Exception
    {
        // You can add more static methods here to throw other customer-related exceptions
        private CustomerExceptions(string message) : base (message)
        {
            // Optional: Add constructor logic for logging or custom error handling
        }

        public static void ThrowCustomerAlreadyExistException(string firstName, string lastName)
        {
            throw new CustomerExceptions($"A client with the name aready exists {firstName} {lastName}.");
        }

        public static void ThrowInvalidCustomerDataException(string message) 
        { 
            throw new CustomerExceptions(message);
        }
    }
}
