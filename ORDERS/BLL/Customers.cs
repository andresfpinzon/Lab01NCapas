using BLL.Exceptions;
using DAL;
using ENTITIES.Models;

namespace BLL
{
    public class Customers
    {
        public async Task<Customer> CreateAsync(Customer customer)
        {
            Customer customerResult = null;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                // Buscar si el nombre de cliente existe
                Customer customerSearch = await repository.RetrieveAsync<Customer>(c=> c.FirstName == customer.FirstName);
                if (customerSearch == null)
                {
                    // No existe, podemos crearlo
                    customerResult = await repository.CreateAsync(customer);
                }
                else
                {
                    // Podríamos aqui lanzar una exepciòn
                    // para notificar que el cliente ya existe.
                    // Podriamos incluso crear una capa de Excepciones
                    // personalizadas y consumirla desde otras  
                    // capas.
                    CustomerExceptions.ThrowCustomerAlreadyExistException(customerSearch.FirstName, customerSearch.LastName);
                }
            }
            return customerResult!;
        }

    }
}
