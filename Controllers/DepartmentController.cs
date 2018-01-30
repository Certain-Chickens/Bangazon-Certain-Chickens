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
purpose: create/read/update for Department
methods:
    GET list of all Departments
    GET single Department
    POST a new Department
    PUT change information on a Department
 */

namespace BangazonAPI.Controllers
{
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        private BangazonContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public DepartmentController(BangazonContext ctx)
        {
            _context = ctx;
        }

        // This method handles GET requests to get all Departments and returns an error if the Department does not exist.
        [HttpGet]
        public IActionResult Get()
        {
            var Departments = _context.Department.ToList();
            if (Departments == null)
            {
                return NotFound();
            }
            return Ok(Departments);
        }

        // This method handles GET requests to get a single Department through searching by id in the db, and returns an error if the Department does not exist.
        [HttpGet("{id}", Name = "GetSingleDepartment")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Department Department = _context.Department.Single(g => g.DepartmentId == id);

                if (Department == null)
                {
                    return NotFound();
                }

                return Ok(Department);
            }
            catch (System.InvalidOperationException ex)
            {
                Console.Write(ex);
                return NotFound();
            }
        }

        // This method handles POST requests add a Department to the Department table
        [HttpPost]
        public IActionResult Post([FromBody]Department Department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Department.Add(Department);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DepartmentExists(Department.DepartmentId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleDepartment", new { id = Department.DepartmentId }, Department);
        }

        // This method handles PUT requests to edit a Department and returns an error if the Department does not exist.
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Department Department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Department.DepartmentId)
            {
                return BadRequest();
            }
            _context.Department.Update(Department);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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

        private bool DepartmentExists(int DepartmentId)
        {
            return _context.Department.Any(g => g.DepartmentId == DepartmentId);
        }

    }
}