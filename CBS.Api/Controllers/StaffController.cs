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
    public class StaffController : ControllerBase
    {
        private IService<Staff, StaffDto> Service { get; set; }
        public StaffController(IService<Staff, StaffDto> service)
        { 
            Service = service;
        
        }
        // GET: api/<StaffController>
        [HttpGet]
        //[Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult< IEnumerable<StaffDto>>> Get()
        {
            try
            {
                var staffs = await Service.All();
                return Ok(staffs);
            }
            catch (ErrorService ex)
            {
                return StatusCode(ex.ErrorCode, ex.ErrorMessage);
            }
        }

        // GET api/<StaffController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult<StaffDto>> Get(int id)
        {
            if (id == 0)
                return BadRequest("Invalid request id field is required");
            try
            {
                var staff = await Service.FindById(id);
                return Ok(staff);
            }
            catch (ErrorService ex)
            {
                return StatusCode(ex.ErrorCode, ex.ErrorMessage);
            }
        }

        // POST api/<StaffController>
        [HttpPost]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult<long>> Post([FromBody] StaffDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request");

            try
                {
                var staff = await Service.Create(dto);
                    return Ok(staff.Id);
                }
                catch (ErrorService ex)
                {
                    return StatusCode(ex.ErrorCode, ex.ErrorMessage);
                }
            }

        // PUT api/<StaffController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult<StaffDto>> Put(long id, [FromBody] StaffDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request data");
            if (id == 0)
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

        // DELETE api/<StaffController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            if (id == 0)
                return BadRequest("Invalid request Id field is required");
            try
            {
                var result = await Service.Remove(id);

                return Ok(result);
            }
            catch (ErrorService ex)
            {
                return StatusCode(ex.ErrorCode, ex.ErrorMessage);
            }
        }
    }
}
