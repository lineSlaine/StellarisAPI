using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StellarisAPI.Models;
using StellarisAPI.Services;

namespace StellarisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(DatabaseContext db) : ControllerBase 
    {
        
        [HttpGet]
        public IActionResult getTests() => Ok(db.testModels);
        [HttpGet("count")]
        public IActionResult getCountTests() => Ok(db.testModels.Count());

        [HttpGet("{id}")]
        public IActionResult getTest(int id)
        {

            var test = db.testModels.Where(result => result.Id == id);
            if (test.Count()==0)
            {
                return NotFound();
            }
            return Ok(test);
        }
        [HttpDelete("{id}")]
        public IActionResult deleteTest(int id)
        {
            var test = db.testModels.Where(result => result.Id == id).ExecuteDelete();
            if (test == 0)
            {
                return NotFound();
            }
            return Ok();
        }
        // если есть какая-то форма
        [HttpPost]
        public IActionResult addTest([FromForm] TestModel test)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.testModels.Add(test);
            db.SaveChanges();
            return CreatedAtAction(nameof(getTest), new { id = test.Id }, test);
        }
        // если json формат
        [HttpPost("add")]
        public IActionResult addTask([FromBody] TestModel test) => addTest(test);
        // если есть какая-то форма
        [HttpPut]
        public IActionResult editTest([FromForm] TestModel test)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var editedTest = db.testModels.Where(result => result.Id == test.Id);
            if (editedTest.Count() == 0)
            {
                return NotFound();
            }
            editedTest.First().Name = test.Name;
            db.SaveChanges();
            return Ok(editedTest);
        }
        // если json формат
        [HttpPut("edit")]
        public IActionResult edittestJson([FromBody] TestModel test) => editTest(test);
    }
}