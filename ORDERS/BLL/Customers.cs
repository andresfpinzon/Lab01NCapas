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
                    CustomerExceptions.ThrowCustomerAlreadyExistsException(customerSearch.FirstName, customerSearch.LastName);
                }
            }
            return customerResult!;
        }

        public async Task<Customer> RetrieveByIDAsync(int id)
        {
            Customer result = null;

            using (var repository = RepositoryFactory.CreateRepository())
            {
                Customer customer = await repository.RetrieveAsync<Customer>(c => c.Id == id);

                // Check if customer was found
                if (customer == null)
                {
                    // Throw a CustomerNotFoundException (assuming you have this class)
                    CustomerExceptions.ThrowInvalidCustomerIdException(id);
                }

                return customer!;
            }
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            bool Result = false;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                // Validar que el nombre del cliente no exista
                Customer customerSearch = await repository.RetrieveAsync<Customer>(
                    c => c.FirstName == customer.FirstName && c.Id != customer.Id);

                if (customerSearch == null)
                {
                    // No existe
                    Result = await repository.UpdateAsync(customer);
                }
                else
                {
                    // Podemos implementar alguna lógica para
                    // indicar que no se pudo modificar
                    CustomerExceptions.ThrowCustomerAlreadyExistsException(
                        customerSearch.FirstName, customerSearch.LastName);
                }
            }
            return Result;
        }


    }
}
