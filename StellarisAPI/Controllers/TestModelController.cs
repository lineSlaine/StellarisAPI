using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StellarisAPI.Models;

namespace StellarisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private static List<TestModel> testList = new List<TestModel>(new[]
        {
            new TestModel() { Id = 0, Name = "Test 0" },
            new TestModel() { Id = 1, Name = "Test 1" },
            new TestModel() { Id = 2, Name = "Test 2" }
        });
        [HttpGet]
        public IActionResult getTests() => Ok(testList);
        [HttpGet("count")]
        public IActionResult getCountTests() => Ok(testList.Count);

        [HttpGet("{id}")]
        public IActionResult getTest(int id)
        {

            var test = testList.SingleOrDefault(a => a.Id == id);
            if (test == null)
            {
                return NotFound();
            }
            return Ok(test);
        }
        [HttpDelete("{id}")]
        public IActionResult deleteTest(int id)
        {
            var test = testList.SingleOrDefault(a => a.Id == id);
            if (test == null)
            {
                return NotFound();
            }
            testList.Remove(test);
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
            test.Id = testList.Count;
            testList.Add(test);
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
            var editedTest = testList.SingleOrDefault(a => a.Id == test.Id);
            if (editedTest == null)
            {
                return NotFound();
            }
            editedTest.Name = test.Name;
            return Ok(editedTest);
        }
        // если json формат
        [HttpPut("edit")]
        public IActionResult edittestJson([FromBody] TestModel test) => editTest(test);
    }
}