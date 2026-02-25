using CBS.BLL.Services;
using CBS.Data.Entities;
using CBS.Model.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CBS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private IService<Patient,PatientDto> Service { get; set; }
        public PatientController(IService<Patient, PatientDto> service) { Service = service; }
        // GET: api/<PatientController>
        [HttpGet]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAll()
        {
            try
            {
                var patients = await Service.All();
                return Ok(patients);
            }
            catch (ErrorService ex)
            {
                return StatusCode(ex.ErrorCode, ex.ErrorMessage);
            }
        }

        // GET api/<PatientController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Staff,User")]
        public async Task<ActionResult<PatientDto>> Get(long id)
        {
            if (id == 0)
                return BadRequest("Invalid request id field is required");
            try
            {
                var patient = await Service.FindById(id);
                return Ok(patient);
            }
            catch (ErrorService ex)
            {
                return StatusCode(ex.ErrorCode, ex.ErrorMessage);
            }
        }

        // POST api/<PatientController>
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<long>>Post([FromBody] PatientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request");
            try
            {
                var patient = await Service.Create(dto);

                return Ok(patient.Id);
            }
            catch (ErrorService ex)
            {
                return StatusCode(ex.ErrorCode, ex.ErrorMessage);
            }
        }

        // PUT api/<PatientController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> Put(long id, [FromBody]  PatientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request data");
            if (id == 0)
                return BadRequest("Invalid request Id field is required");
            try
            {
                 await Service.Modify(id,dto);

                return Ok();
            }
            catch (ErrorService ex)
            {
                return StatusCode(ex.ErrorCode, ex.ErrorMessage);
            }
        }

        // DELETE api/<PatientController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<bool> >Delete(long id)
        {
            if (id == 0)
                return BadRequest("Invalid request Id field is required");
            try
            {
               var result= await Service.Remove(id);

                return Ok(result);
            }
            catch (ErrorService ex)
            {
                return StatusCode(ex.ErrorCode, ex.ErrorMessage);
            }
        }
    }
}
