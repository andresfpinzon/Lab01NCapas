using BLL.Exceptions;
using DAL;
using ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Suppliers
    {
        public async Task<Supplier> CreateAsync(Supplier supplier)
        {
            Supplier supplierResult = null;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                // Buscar si el nombre de cliente existe
                Supplier supplierSearch = await repository.RetrieveAsync<Supplier>(c => c.CompanyName == supplier.CompanyName);
                if (supplierSearch == null)
                {
                    // No existe, podemos crearlo
                    supplierResult = await repository.CreateAsync(supplier);
                }
                else
                {
                    // Podríamos aqui lanzar una exepciòn
                    // para notificar que el cliente ya existe.
                    // Podriamos incluso crear una capa de Excepciones
                    // personalizadas y consumirla desde otras  
                    // capas.
                    SupplierExceptions.ThrowSupplierAlreadyExistsException(supplierSearch.CompanyName);
                }
            }
            return supplierResult!;
        }

        public async Task<Supplier> RetrieveByIDAsync(int id)
        {
            Supplier result = null;

            using (var repository = RepositoryFactory.CreateRepository())
            {
                Supplier supplier = await repository.RetrieveAsync<Supplier>(c => c.Id == id);

                // Check if customer was found
                if (supplier == null)
                {
                    // Throw a CustomerNotFoundException (assuming you have this class)
                    SupplierExceptions.ThrowInvalidSupplierIdException(id);
                }

                return supplier!;
            }
        }

        public async Task<bool> UpdateAsync(Supplier supplier)
        {
            bool Result = false;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                // Validar que el nombre del cliente no exista
                Supplier supplierSearch = await repository.RetrieveAsync<Supplier>(
                    c => c.CompanyName == supplier.CompanyName && c.Id != supplier.Id);

                if (supplierSearch == null)
                {
                    // No existe
                    Result = await repository.UpdateAsync(supplier);
                }
                else
                {
                    // Podemos implementar alguna lógica para
                    // indicar que no se pudo modificar
                    SupplierExceptions.ThrowSupplierAlreadyExistsException(
                        supplierSearch.CompanyName);
                }
            }
            return Result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool Result = false;
            // Buscar un cliente para ver si tiene Orders (Ordenes de Compra)
            var supplier = await RetrieveByIDAsync(id);
            if (supplier != null)
            {
                // Eliminar el cliente
                using (var repository = RepositoryFactory.CreateRepository())
                {
                    Result = await repository.DeleteAsync(supplier);
                }
            }
            else
            {
                // Podemos implementar alguna lógica
                // para indicar que el producto no existe
                SupplierExceptions.ThrowInvalidSupplierIdException(id);
            }
            return Result;
        }

        public async Task<List<Supplier>> RetrieveAllAsync()
        {
            List<Supplier> Result = null;

            using (var r = RepositoryFactory.CreateRepository())
            {
                // Define el criterio de filtro para obtener todos los clientes.
                Expression<Func<Supplier, bool>> allSupplierCriteria = x => true;
                Result = await r.FilterAsync<Supplier>(allSupplierCriteria);
            }
            return Result;
        }
    }
}
