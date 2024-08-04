using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class ProductExceptions : Exception
    {
        // You can add more static methods here to throw other customer-related exceptions
        private ProductExceptions(string message) : base(message)
        {
            // Optional: Add constructor logic for logging or custom error handling
        }

        public static void ThrowProductAlreadyExistsException(string productName)
        {
            throw new ProductExceptions($"A Product with the name {productName} already exists.");
        }

        public static void ThrowInvalidProductDataException(string message)
        {
            throw new ProductExceptions(message);
        }

        public static void ThrowInvalidProductIdException(int id)
        {
            throw new ProductExceptions($"A Product with the Id {id} not exists .");
        }
    }
}
