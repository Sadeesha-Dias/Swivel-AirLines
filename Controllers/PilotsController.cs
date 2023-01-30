using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swivel_AirLines.Models;



namespace Swivel_AirLines.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class PilotsController : ControllerBase
    {
        private readonly ILogger<PilotsController> _logger;
        private static List<Pilots> pilots = new List<Pilots>();

        public PilotsController(ILogger<PilotsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// retrive all the pilots
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/v1/pilots")]
        public IActionResult GetAllPilots()
        {
            var allPilots = pilots.Where(c => c.Status == 1).ToList();
            return Ok(allPilots);
        }

        /// <summary>
        /// Add a new pilot
        /// </summary>
        /// <param name="pilotsData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/v1/addpilot")]
        public IActionResult AddPilot(Pilots pilotsData)
        {
            if (ModelState.IsValid)
            {
                pilots.Add(pilotsData);
                //return CreatedAtAction(nameof(AddPilot), new { pilotsData.Id }, pilotsData);
                return Ok(pilots);
            }

            return new JsonResult("Unable to add new pilot") { StatusCode = 500 };
        }

        /// <summary>
        /// get a pilot by the piolt's id
        /// </summary>
        /// <param name="pilotId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/v1/pilot/{pilotId}")]
        public IActionResult GetPilotById(Guid pilotId)
        {
            var pilot = pilots.FirstOrDefault(c => c.Id == pilotId);

            if (pilot == null)
            {
                return NotFound();
            }
            return Ok(pilot);

            
        }

        /// <summary>
        /// update an existing pilot
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pilotsData"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/v1/editpilot/{id}")]
        public IActionResult UpdatePilot(Guid id, Pilots pilotsData)
        {
            //check if the passing id is different from the pilot's id
            if (id != pilotsData.Id)
            {
                return BadRequest();
            }

            //check if the pilot exists or not
            var existingPilot = pilots.FirstOrDefault(c => c.Id.Equals(id));
            if (existingPilot == null)
            {
                return new JsonResult("Pliot not found") { StatusCode = 404 };
            }

            existingPilot.PilotLisenceNumber = pilotsData.PilotLisenceNumber;
            existingPilot.FirstName = pilotsData.FirstName;
            existingPilot.LastName = pilotsData.LastName;
            existingPilot.FlyingHoures = pilotsData.FlyingHoures;

            return Ok(existingPilot); //can use "return NoContent()" instead.
        }

        [HttpDelete]
        [Route("api/v1/deletepilot/{id}")]
        public IActionResult DeletePilotById(Guid id)
        {
            var existingPilot = pilots.FirstOrDefault(c => c.Id == id);

            if (existingPilot == null)
            {
                return NotFound();
            }

            existingPilot.Status = 0; //pilot is removed --> 1 = Pilot Active | 0 = Pilot Removed

            return Ok(existingPilot); //can use "return NoContent()" instead.
        }
        
            
        
    }
}
