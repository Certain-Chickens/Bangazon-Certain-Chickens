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
purpose: create/read/update/delete for Employees
methods:
    GET list of all Employees
    GET single Employee
    POST a new Employee
    PUT change information on an Employee
    DELETE an Employee
 */

namespace BangazonAPI.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private BangazonContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public EmployeeController(BangazonContext ctx)
        {
            _context = ctx;
        }

        // This method handles GET requests to get all Employee and returns an error if the Employee does not exist.
        [HttpGet]
        public IActionResult Get()
        {
            var Employee = _context.Employee.ToList();
            if (Employee == null)
            {
                return NotFound();
            }
            return Ok(Employee);
        }

        // This method handles GET requests to get a single Employee through searching by id in the db, and returns an error if the Employee does not exist.
        [HttpGet("{id}", Name = "GetSingleEmployee")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Employee Employee = _context.Employee.Single(g => g.EmployeeId == id);

                if (Employee == null)
                {
                    return NotFound();
                }

                return Ok(Employee);
            }
            catch (System.InvalidOperationException ex)
            {
                Console.Write(ex);
                return NotFound();
            }
        }

        // This method handles POST requests add a Employee to the Employee table
        [HttpPost]
        public IActionResult Post([FromBody]Employee Employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Employee.Add(Employee);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(Employee.EmployeeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleEmployee", new { id = Employee.EmployeeId }, Employee);
        }

        // This method handles PUT requests to edit an Employee and returns an error if the Employee does not exist.
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Employee Employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Employee.EmployeeId)
            {
                return BadRequest();
            }
            _context.Employee.Update(Employee);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        private bool EmployeeExists(int EmployeeId)
        {
            return _context.Employee.Any(g => g.EmployeeId == EmployeeId);
        }

    }
}