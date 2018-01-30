using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BangazonAPI.Data;
using BangazonAPI.Models;
using Microsoft.AspNetCore.Cors;
using System.Web.Http.Cors;

/* Author: Ryan McPherson, Keith Davis, Leah Duvic, Kevin Haggerty
purpose: create/read/update/delete for Orders
methods:
    GET list of all Orders
    GET single Orders with associated products
    POST a new Order
    PUT change information on a Order
    DELETE an Order
 */

namespace BangazonAPI.Controllers
{
    [Route("api/[controller]")]
    [System.Web.Http.Cors.EnableCors(origins: "http://bangazon.com", headers: "accept,content-type,origin,x-my-header", methods: "*")]
    public class OrdersController : Controller
    {
        private BangazonContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public OrdersController(BangazonContext ctx)
        {
            _context = ctx;
        }

        // This method handles GET requests to get all Orders and returns an error if the Order does not exist.
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

        // This method handles GET requests to get a single Order through searching by id in the db, and returns an error if the Order does not exist.
        [HttpGet("{id}", Name = "GetSingleOrders")]
        public IActionResult Get(int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var order =
                _context.Orders.Where(o => o.OrderId == id)
                .Select(o => new {
                    OrderId = o.OrderId,
                    CustomerId = o.CustomerId,
                    PaymentTypeId = o.PaymentTypeId,
                    Products = o.OrderProduct.Select(op => new {
                        ProductId = op.Product.ProductId,
                        Name = op.Product.Title,
                        Price = op.Product.Price,
                        Quantity = op.Product.Quantity
                    })
                });

                if (order == null)
                {
                    return NotFound();
                }

                return Ok(order);

            }
            catch (System.InvalidOperationException ex)
            {
                Console.Write(ex);
                return NotFound();
            }
        }

        // This method handles POST requests add an Order to the Order table
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

        // This method handles PUT requests to edit an Order and returns an error if the Order does not exist.
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
                if (!OrdersExists(Orders.OrderId))
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

        // This method handles DELETE requests to delete a single Order and returns an error if the Order does not exist.
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