using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BangazonAPI.Data;
using BangazonAPI.Models;

/* Author: Ryan McPherson
purpose: create/read/update/delete for Products
methods:
    GET list of all Products
    GET single Product
    POST a new Product
    PUT change information on a Product
    DELETE a Product
 */

namespace BangazonAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private BangazonContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public ProductController(BangazonContext ctx)
        {
            _context = ctx;
        }

        // This method handles GET requests to get all Products and returns an error if the Product does not exist.
        [HttpGet]
        public IActionResult Get()
        {
            var Product = _context.Product.ToList();
            if (Product == null)
            {
                return NotFound();
            }
            return Ok(Product);
        }
        // This method handles GET requests to get a single Product through searching by id in the db, and returns an error if the Product does not exist.
        [HttpGet("{id}", Name = "GetSingleProduct")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Product Product = _context.Product.Single(g => g.ProductId == id);

                if (Product == null)
                {
                    return NotFound();
                }

                return Ok(Product);
            }
            catch (System.InvalidOperationException ex)
            {
                Console.WriteLine(ex);
                return NotFound();
            }
        }

        // This method handles POST requests add a Product to the Product table
        [HttpPost]
        public IActionResult Post([FromBody]Product Product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Product.Add(Product);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(Product.ProductId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleProduct", new { id = Product.ProductId }, Product);
        }

        // This method handles PUT requests to edit an Product and returns an error if the Product does not exist.
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Product Product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Product.ProductId)
            {
                return BadRequest();
            }
            _context.Product.Update(Product);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // This method handles DELETE requests to delete a single Product and returns an error if the Product does not exist.
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product Product = _context.Product.Single(g => g.ProductId == id);

            if (Product == null)
            {
                return NotFound();
            }
            _context.Product.Remove(Product);
            _context.SaveChanges();
            return Ok(Product);
        }

        private bool ProductExists(int ProductId)
        {
            return _context.Product.Any(g => g.ProductId == ProductId);
        }

    }
}