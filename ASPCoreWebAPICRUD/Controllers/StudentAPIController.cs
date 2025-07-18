using ASPCoreWebAPICRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace ASPCoreWebAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly MyDbContext context;

        public StudentAPIController(MyDbContext context)
        {
            this.context = context;
        }

        // GET: api/StudentAPI
        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            var data = await context.Students.ToListAsync();
            return Ok(data);
        }

        // GET: api/StudentAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        // POST: api/StudentAPI
        [HttpPost]
        public async Task<ActionResult<Student>> AddStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest("Student object is null");
            }

            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();

            // Return 201 Created with the URI to access the newly created student
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student std)
        {
            if (id != std.Id)
                return BadRequest("Id in URL and body do not match.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            context.Entry(std).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return Ok(std);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var std = await context.Students.FindAsync(id);
            if (std == null)
            {
                return NotFound();
            }

            context.Students.Remove(std);
            await context.SaveChangesAsync();
            return Ok();
        }

    }


}