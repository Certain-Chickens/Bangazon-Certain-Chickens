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
purpose: create/read/update/delete for ProductTypes
methods:
    GET list of all ProductTypes
    GET single ProductType
    POST a new ProductType
    PUT change information on a ProductType
    DELETE a ProductType
 */

namespace BangazonAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductTypeController : Controller
    {
        private BangazonContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public ProductTypeController(BangazonContext ctx)
        {
            _context = ctx;
        }
        // This method handles GET requests to get all productTypes and returns an error if the product does not exist.
        [HttpGet]
        public IActionResult Get()
        {
            var ProductType = _context.ProductType.ToList();
            if (ProductType == null)
            {
                return NotFound();
            }
            return Ok(ProductType);
        }

        // This method handles GET requests to get a single productType through searching by id in the db, and returns an error if the customer does not exist.
        [HttpGet("{id}", Name = "GetSingleProductType")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ProductType ProductType = _context.ProductType.Single(g => g.ProductTypeId == id);

                if (ProductType == null)
                {
                    return NotFound();
                }

                return Ok(ProductType);
            }
            catch (System.InvalidOperationException ex)
            {
                Console.WriteLine(ex);
                return NotFound();
            }
        }

        // This method handles POST requests add a productType to the ProductType table
        [HttpPost]
        public IActionResult Post([FromBody]ProductType ProductType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProductType.Add(ProductType);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductTypeExists(ProductType.ProductTypeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleProductType", new { id = ProductType.ProductTypeId }, ProductType);
        }

        // This method handles PUT requests to edit a productType and returns an error if the productType does not exist.
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ProductType ProductType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ProductType.ProductTypeId)
            {
                return BadRequest();
            }
            _context.ProductType.Update(ProductType);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTypeExists(id))
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

        // This method handles DELETE requests to delete a single productType and returns an error if the productType does not exist.
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ProductType ProductType = _context.ProductType.Single(g => g.ProductTypeId == id);

            if (ProductType == null)
            {
                return NotFound();
            }
            _context.ProductType.Remove(ProductType);
            _context.SaveChanges();
            return Ok(ProductType);
        }

        private bool ProductTypeExists(int ProductTypeId)
        {
            return _context.ProductType.Any(g => g.ProductTypeId == ProductTypeId);
        }

    }
}