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
Author: Greg Turner
purpose: Create/Read/Update/Delete for the Computer table in BANGAZON_DB
methods: 
    GET list of all Computers
    GET single Computer
    POST a new Computer
    PUT update information on a Computer
    DELETE a single Computer
 */

 // GET api from Computer model
namespace BangazonAPI.Controllers
{
    [Route("api/[controller]")]
    public class ComputerController : Controller
    {
        private BangazonContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public ComputerController(BangazonContext ctx)
        {
            _context = ctx;
        }
        // This method handles GET requests to GET a list of computers 
        [HttpGet]
        public IActionResult Get()
        {
            var Computers = _context.Computer.ToList();
            if (Computers == null)
            {
                return NotFound();
            }
            return Ok(Computers);
        }

        // This method is using GET to retrieve a single Computer
        [HttpGet("{id}", Name = "GetSingleComputer")]
        public IActionResult Get(int id)
        {
            // error to handle if the user input the correct info in order to use the api
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // search database to try and find a match for the computer id entered
            try
            {
                Computer Computer = _context.Computer.Single(g => g.ComputerId == id);

                if (Computer == null)
                {
                    return NotFound();
                }

                return Ok(Computer);
            }
            catch (System.InvalidOperationException ex)
            {
                Console.Write(ex);
                return NotFound();
            }
        }

        /* This method handles POST requests to add a computer,
        saves it and throws an error if it already exists. */
        [HttpPost]
        public IActionResult Post([FromBody]Computer Computer)
        {
            // error to handle if the user input the correct info in order to use the api
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // save computer to BANGAZON_DB 
            _context.Computer.Add(Computer);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                // check if the computer Id already exists in the database and throw an error
                if (ComputerExists(Computer.ComputerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleComputer", new { id = Computer.ComputerId }, Computer);
        }

        /* This method handles PUT requests to edit a single computer through searching by id in the db,
        saves modifications and returns an error if the computer does not exist. */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Computer Computer)
        {
            // error to handle if the user input the correct info in order to use the api
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Computer.ComputerId)
            {
                return BadRequest();
            }
            _context.Computer.Update(Computer);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComputerExists(id))
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

        /* This method handles DELETE requests to delete a single computer through searching by id in the db,
        removes computer and returns an error if the computer does not exist. */
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Computer Computer = _context.Computer.Single(g => g.ComputerId == id);

            if (Computer == null)
            {
                return NotFound();
            }
            _context.Computer.Remove(Computer);
            _context.SaveChanges();
            return Ok(Computer);
        }

        private bool ComputerExists(int ComputerId)
        {
            return _context.Computer.Any(g => g.ComputerId == ComputerId);
        }
        
    }
}