using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonAPI.Data;
using BangazonAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/*Author: Leah Duvic and Greg Turner
purpose: add/update/delete for Customer
methods: 
    GET list of all Customers
    GET single Customer
    POST a new Customer
    PUT change information on a Customer
 */

// GET api from customer model
namespace BangazonAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private BangazonContext context;

        public CustomerController(BangazonContext ctx)
        {
            context = ctx;
        }

         // This method handles GET requests to retrieve a single customer through searching by id in the db, add customer and returns an error if the customer does not exist. 
        [HttpGet]
        public IActionResult Get(bool? active)
        {
            
            if (active == null)
            {
                var customers = context.Customer.ToList();
                if (customers == null)
                {
                    return NotFound();
                }
                return Ok(customers);
            }
            
            // Then by using the URL parameter /customers/?active=false, the JSON response only contains the customers that don't have any orders placed yet.
            else
            {
                // first, search for all customers that have an order
                var activeCustomer =
                from o in context.Orders
                join c in context.Customer on o.CustomerId equals c.CustomerId
                select c;

                // THEN list the customers that have never placed an order by eliminating the active customers.
                var innactiveCustomers = context.Customer.Except(activeCustomer);

                return Ok(innactiveCustomers);
            }
        }

        // This method is using GET to retrieve a single customer
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get([FromRoute] int id)
        {
            // error to handle if the user input the correct info in order to use the api.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // search database to try and find a match for the customer id entered
            try
            {
                Customer customer = context.Customer.Single(m => m.CustomerId == id);

                if (customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }
            catch (System.InvalidOperationException ex)
            {
                Console.Write(ex);
                return NotFound();
            }


        }

        // This method handles POST requests to add a customer, saves it and throws an error if it already exists.

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            // save customer to db 
            context.Customer.Add(customer);

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                // check if the customer Id already exists in the database and throw an error
                if (CustomerExists(customer.CustomerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // This method handles PUT requests to edit a single customer through searching by id in the db, saves modifications and returns an error if the customer does not exist. 
        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            context.Entry(customer).State = EntityState.Modified;

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        // This method handles DELETE requests to delete a single customer through searching by id in the db, removes customer and returns an error if the customer does not exist. 

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Customer customer = context.Customer.Single(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            context.Customer.Remove(customer);
            context.SaveChanges();

            return Ok(customer);
        }

        private bool CustomerExists(int id)
        {
            return context.Customer.Count(e => e.CustomerId == id) > 0;
        }

    }
}
