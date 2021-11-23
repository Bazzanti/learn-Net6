using FormazioneNET6.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FormazioneNET6.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext db;
        private readonly EmployeeInterface _service;

        public EmployeeController(EmployeeContext context, EmployeeInterface service)
        {
            db = context;
            _service = service;
        }

        [HttpGet("")]
        public async Task<List<Employee>> GetList()
        {
            return await _service.GetListAsync();
        }

        [HttpGet("/{id}")]
        public async Task<Employee> GetById(int id)
        {
            return await _service.FindAsync(id);
        }

        [HttpPost("")]
        public async Task<IResult> Add([FromBody]Employee employee)
        {
            await _service.Add(employee);
            return Results.Accepted();
        }

        [HttpPut("")]
        public async Task<IResult> Update([FromBody] Employee employee)
        {
            var result = await _service.Update(employee);

            if(result == null)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        }

        [HttpDelete("")]
        public async Task<IResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (!result)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        }

    }
}
