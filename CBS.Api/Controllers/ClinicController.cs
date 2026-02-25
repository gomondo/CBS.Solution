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
    public class ClinicController : ControllerBase
    {
        private IService<Clinic, ClinicDto> Service { get; set; }
        public ClinicController(IService<Clinic, ClinicDto> service)
        {
            this.Service = service;
        }
        // GET: api/<ClinicController>
        [HttpGet]
        [Authorize(Roles = "Admin,Staff,User")]
        public async Task<ActionResult<IEnumerable<ClinicDto>>> GetAll()
        {
            try
            {
                var clinics = await Service.All();
                return Ok(clinics);
            }
            catch (ErrorService ex)
            {
                return StatusCode(ex.ErrorCode, ex.ErrorMessage);
            }
        }

        // GET api/<ClinicController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Staff,User")]
        public async Task<ActionResult<ClinicDto>> Get(long id)
        {
            if (id == 0)
                return BadRequest("Invalid request id field is required");
            try
            {
                var clinic = await Service.FindById(id);
                return Ok(clinic);
            }
            catch (ErrorService ex)
            {
                return StatusCode(ex.ErrorCode, ex.ErrorMessage);
            }
        }

        // POST api/<ClinicController>
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> Post([FromBody] ClinicDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request");
            try
            {
                var clinic = await Service.Create(dto);
                return Ok(clinic);
            }
            catch (ErrorService ex)
            {
                return StatusCode(ex.ErrorCode, ex.ErrorMessage);
            }
        }

        // PUT api/<ClinicController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async  Task<ActionResult> Put([FromQuery] long id, [FromBody] ClinicDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request data");
            if(id==0)
                return BadRequest("Invalid request Id field is required");
            try
            {
                await Service.Modify(id, dto);
                return Ok();
            }
            catch (ErrorService ex)
            {
                return StatusCode(ex.ErrorCode, ex.ErrorMessage);
            }
        }

        // DELETE api/<ClinicController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> Delete(long id)
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
