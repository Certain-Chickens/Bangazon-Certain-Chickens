using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BangazonAPI.Data;
using BangazonAPI.Models;

/*
Author: Kevin Haggerty
purpose: Create/Read/Update/Delete for the PaymentType table in BANGAZON_DB
methods: 
    GET list of all PaymentTypes
    GET single PaymentType
    POST a new PaymentType
    PUT update information on a PaymentType
    DELETE a single PaymentType
 */

 // GET api from PaymentType model
namespace BangazonAPI.Controllers
{
    [Route("api/[controller]")]
    public class PaymentTypeController : Controller
    {
        private BangazonContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public PaymentTypeController(BangazonContext ctx)
        {
            _context = ctx;
        }
        // This method handles GET requests to GET a list of PaymentTypes
        [HttpGet]
        public IActionResult Get()
        {
            var paymentType = _context.PaymentType.ToList();
            if (paymentType == null)
            {
                return NotFound();
            }
            return Ok(paymentType);
        }

        // This method is using GET to retrieve a single PaymentType
        [HttpGet("{id}", Name = "GetSinglePaymentType")]
        public IActionResult Get(int id)
        {
            // error to handle if the user input the correct info in order to use the api
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // search database to try and find a match for the paymenttype id entered
            try
            {
                PaymentType paymentType = _context.PaymentType.Single(g => g.PaymentTypeId == id);

                if (paymentType == null)
                {
                    return NotFound();
                }

                return Ok(paymentType);
            }
            catch (System.InvalidOperationException ex)
            {
                Console.WriteLine(ex);
                return NotFound();
            }
        }

        /* This method handles POST requests to add a paymenttype,
        saves it and throws an error if it already exists. */
        [HttpPost]
        public IActionResult Post([FromBody]PaymentType paymentType)
        {
            // error to handle if the user input the correct info in order to use the api
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PaymentType.Add(paymentType);

            try
            {
                // save paymenttype to BANGAZON_DB 
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                // check if the paymenttype Id already exists in the database and throw an error
                if (PaymentTypeExists(paymentType.PaymentTypeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSinglePaymentType", new { id = paymentType.PaymentTypeId }, paymentType);
        }

        /* This method handles PUT requests to edit a single paymenttype through searching by id in the db,
        saves modifications and returns an error if the paymenttype does not exist. */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]PaymentType paymentType)
        {
            // error to handle if the user input the correct info in order to use the api
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paymentType.PaymentTypeId)
            {
                return BadRequest();
            }
            _context.PaymentType.Update(paymentType);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentTypeExists(id))
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

        /* This method handles DELETE requests to delete a single paymenttype through searching by id in the db,
        removes paymenttype and returns an error if the paymenttype does not exist. */
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            PaymentType paymentType = _context.PaymentType.Single(g => g.PaymentTypeId == id);

            if (paymentType == null)
            {
                return NotFound();
            }
            _context.PaymentType.Remove(paymentType);
            _context.SaveChanges();
            return Ok(paymentType);
        }

        private bool PaymentTypeExists(int paymentTypeId)
        {
            return _context.PaymentType.Any(g => g.PaymentTypeId == paymentTypeId);
        }
        
    }
}