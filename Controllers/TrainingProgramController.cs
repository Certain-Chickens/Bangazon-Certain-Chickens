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
purpose: Create/Read/Update/Delete for the TrainingProgram table in BANGAZON_DB
methods: 
    GET list of all TrainingPrograms
    GET single TrainingProgram
    POST a new TrainingProgram
    PUT update information on a TrainingProgram
    DELETE a single TrainingProgram
 */

 // GET api from TrainingProgram model
namespace BangazonAPI.Controllers
{
    [Route("api/[controller]")]
    public class TrainingProgramController : Controller
    {
        private BangazonContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public TrainingProgramController(BangazonContext ctx)
        {
            _context = ctx;
        }
        // This method handles GET requests to GET a list of TrainingPrograms
        [HttpGet]
        public IActionResult Get()
        {
            var trainingProgram = _context.TrainingProgram.ToList();
            if (trainingProgram == null)
            {
                return NotFound();
            }
            return Ok(trainingProgram);
        }

        // This method is using GET to retrieve a single TrainingProgram
        [HttpGet("{id}", Name = "GetSingleTrainingProgram")]
        public IActionResult Get(int id)
        {
            // error to handle if the user input the correct info in order to use the api
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // search database to try and find a match for the trainingprogram id entered
            try
            {
                TrainingProgram trainingProgram = _context.TrainingProgram.Single(g => g.TrainingProgramId == id);

                if (trainingProgram == null)
                {
                    return NotFound();
                }

                return Ok(trainingProgram);
            }
            catch (System.InvalidOperationException ex)
            {
                Console.Write(ex);
                return NotFound();
            }
        }

        /* This method handles POST requests to add a trainingprogram,
        saves it and throws an error if it already exists. */
        [HttpPost]
        public IActionResult Post([FromBody]TrainingProgram trainingProgram)
        {
            // error to handle if the user input the correct info in order to use the api
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // save trainingprogram to BANGAZON_DB 
            _context.TrainingProgram.Add(trainingProgram);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                // check if the trainingprogram Id already exists in the database and throw an error
                if (TrainingProgramExists(trainingProgram.TrainingProgramId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleTrainingProgram", new { id = trainingProgram.TrainingProgramId }, trainingProgram);
        }

        /* This method handles PUT requests to edit a single trainingprogram through searching by id in the db,
        saves modifications and returns an error if the trainingprogram does not exist. */
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TrainingProgram trainingProgram)
        {
            // error to handle if the user input the correct info in order to use the api
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trainingProgram.TrainingProgramId)
            {
                return BadRequest();
            }
            _context.TrainingProgram.Update(trainingProgram);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingProgramExists(id))
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

        /* This method handles DELETE requests to delete a single trainingprogram through searching by id in the db,
        removes trainingprogram and returns an error if the trainingprogram does not exist. */
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TrainingProgram trainingProgram = _context.TrainingProgram.Single(g => g.TrainingProgramId == id);

            if (trainingProgram == null)
            {
                return NotFound();
            }
           
            DateTime date1 = DateTime.Now;
            DateTime date2 = trainingProgram.StartDate;
            int result = DateTime.Compare(date1, date2);

            if (result >= 0)
            {
                return BadRequest();  
            }
            else 
            {
            _context.TrainingProgram.Remove(trainingProgram);
            _context.SaveChanges();
            return Ok(trainingProgram);

            }
        }
        private bool TrainingProgramExists(int trainingProgramId)
        {
            return _context.TrainingProgram.Any(g => g.TrainingProgramId == trainingProgramId);
        }
        
    }
}