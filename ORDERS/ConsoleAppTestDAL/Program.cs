// See https://aka.ms/new-console-template for more information
using DAL;
using ENTITIES.Models;
using System.Linq.Expressions;

CreateAsync().GetAwaiter().GetResult();
RetrieveAsync().GetAwaiter().GetResult();

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