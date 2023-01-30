using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swivel_AirLines.DTO.IncomingDTO;
using Swivel_AirLines.DTO.OutgoingDTO;
using Swivel_AirLines.Models;



namespace Swivel_AirLines.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class PilotsController : ControllerBase
    {
        private readonly ILogger<PilotsController> _logger;
        private static List<Pilots> pilots = new List<Pilots>();
        private readonly IMapper _mapper;

        public PilotsController(ILogger<PilotsController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
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

            // Taking the entire object list of Pilots and mapping it with "PilotOutcomingDto" and returnig the response.
            var allPilotResponse = _mapper.Map<IEnumerable<PilotOutgoingDto>>(allPilots);
            return Ok(allPilotResponse);
        }

        /// <summary>
        /// Add a new pilot
        /// </summary>
        /// <param name="pilotsData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/v1/addpilot")]
        public IActionResult AddPilot(PilotIncomingDto pilotsData)
        {
            if (ModelState.IsValid)
            {
                // manual mapping without AutoMapper
                /*var _newPilot = new Pilots()
                {
                    Id = Guid.NewGuid(),
                    Status = 1,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,

                    FirstName = pilotsData.FirstName,
                    LastName = pilotsData.LastName,
                    PilotLisenceNumber = pilotsData.PilotLisenceNumber,
                    FlyingHours = pilotsData.FlyingHours,
                }; */

                // --> Creating an object which its type is "Pilots" from the "pilotsData" object which is created from "PilotIncomingDto".
                var _newPilot = _mapper.Map<Pilots>(pilotsData); 
                pilots.Add(_newPilot);

                // --> Creating the response by the object which its type is "PilotOutgoingDto" from the mapping value of "_newPilot" variable.
                var _newPilotList = _mapper.Map<PilotOutgoingDto>(_newPilot);

                /* var _newPilotList = _mapper.Map<IEnumerable<PilotOutgoingDto>>(pilots); // --> this one gives full list of pilots as the response. */

                return Ok(_newPilotList);
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
            existingPilot.FlyingHours = pilotsData.FlyingHours;

            return Ok(existingPilot); //can use "return NoContent()" instead.
        }

        /// <summary>
        /// Remove the pilot
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
