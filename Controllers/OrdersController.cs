using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BangazonAPI.Data;
using BangazonAPI.Models;

namespace BangazonAPI.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private BangazonContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public OrdersController(BangazonContext ctx)
        {
            _context = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var Orders = _context.Orders.ToList();
            if (Orders == null)
            {
                return NotFound();
            }

            return Ok(Orders);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetSingleOrders")]
        public IActionResult Get(int id)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                IQueryable<object> MainList = _context.Orders.Include("OrderProduct.");

                Orders Orders = _context.Orders
                        .Include(o => o.OrderId == OrderProduct.OrdersId)
                        .Single(g => g.OrderId == OrderProduct.OrdersId);
                {
                     _context.Product.Single(p => p.CustomerId == Orders.OrderId);
                };

            // SELECT * from Orders, Product
            // where Orders.CustomerId = Product.CustomerId

                if (Orders == null)
                {
                    return NotFound();
                }

                return Ok(Orders);

            }
            catch (System.InvalidOperationException ex)
            {
                Console.Write(ex);
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Orders Orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Orders.Add(Orders);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrdersExists(Orders.OrderId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleOrders", new { id = Orders.OrderId }, Orders);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Orders Orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Orders.OrderId)
            {
                return BadRequest();
            }
            _context.Orders.Update(Orders);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersExists(id))
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

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Orders Orders = _context.Orders.Single(g => g.OrderId == id);

            if (Orders == null)
            {
                return NotFound();
            }
            _context.Orders.Remove(Orders);
            _context.SaveChanges();
            return Ok(Orders);
        }

        private bool OrdersExists(int OrdersId)
        {
            return _context.Orders.Any(g => g.OrderId == OrdersId);
        }



    }
}