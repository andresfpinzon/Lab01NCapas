using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class SupplierExceptions : Exception
    {
        // You can add more static methods here to throw other customer-related exceptions
        private SupplierExceptions(string message) : base(message)
        {
            // Optional: Add constructor logic for logging or custom error handling
        }

        public static void ThrowSupplierAlreadyExistsException(string companyName)
        {
            throw new SupplierExceptions($"A Supplier with the name {companyName} already exists.");
        }

        public static void ThrowInvalidSupplierDataException(string message)
        {
            throw new SupplierExceptions(message);
        }

        public static void ThrowInvalidSupplierIdException(int id)
        {
            throw new SupplierExceptions($"A Supplier with the Id {id} not exists .");
        }
    }
}
