// See https://aka.ms/new-console-template for more information
using DAL;
using ENTITIES.Models;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

//CreateAsync().GetAwaiter().GetResult();
//RetrieveAsync().GetAwaiter().GetResult();
//UpdateAsync().GetAwaiter().GetResult();
//FilterAsync().GetAwaiter().GetResult();
DeleteAsync().GetAwaiter().GetResult();
static async Task CreateAsync()
{
    //Add Customer
    Customer customer = new Customer() {
        FirstName = "Andres",
        LastName = "Pinzon",
        City= "Bogotá",
        Country = "Colombia",
        Phone = "3114446666"
    };

    using(var repository = RepositoryFactory.CreateRepository())
    {
        try
        {
            var createdCustomer = await repository.CreateAsync(customer);
            Console.WriteLine($"Added Customer: {createdCustomer.FirstName} {createdCustomer.LastName} ");
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

static async Task RetrieveAsync()
{
    using (var repository = RepositoryFactory.CreateRepository())
    {
        try
        {
            Expression<Func<Customer, bool>> criteria = c => c.FirstName == "Andres" && c.LastName == "Pinzon";
            var customer = await repository.RetrieveAsync(criteria);
            if (customer != null) 
            {
                Console.WriteLine($"Retrived customer: {customer.FirstName} \t{customer.LastName}\t City: {customer.City}\t Country: {customer.Country}");
            }
            Console.WriteLine($"Customer not exist"); 
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

static async Task UpdateAsync()
{
    //supuesto: Existe el objeto a modificar
    using (var repository = RepositoryFactory.CreateRepository()) 
    {
        var customerToUpdate = await repository.RetrieveAsync<Customer>(c => c.Id == 78);
        if (customerToUpdate != null)
        {
            customerToUpdate.FirstName = "Liu";
            customerToUpdate.LastName = "Wong";
            customerToUpdate.City = "Toronto";
            customerToUpdate.Country = "Canada";
            customerToUpdate.Phone = "+14337 6353039";
        }

        try
        {
            bool updated = await repository.UpdateAsync(customerToUpdate);
            if (updated)
            {
                Console.WriteLine("Cusomer updated successfully.");
            }
            else
            {
                Console.WriteLine("Customer update failed");
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

static async Task FilterAsync()
{
    using (var Repository = RepositoryFactory.CreateRepository())
    {
        Expression<Func<Customer, bool>> criteria = c => c.Country == "USA";

        var customers = await Repository.FilterAsync(criteria);

        foreach (var customer in customers) 
        {
            Console.WriteLine($"Customer: {customer.FirstName} {customer.LastName} \t from {customer.City}");
        }
    }
}

static async Task DeleteAsync()
{
    using (var repository = RepositoryFactory.CreateRepository())
    {
        Expression<Func<Customer, bool>> criteria = customer => customer.Id == 98;
        var customerToDelete = await repository.RetrieveAsync(criteria);
        if (customerToDelete != null)
        {
            bool deleted = await repository.DeleteAsync(customerToDelete);
            Console.WriteLine(deleted ? "Customer deleted successfully" : "Failed to delete customer");
        }
    }
}