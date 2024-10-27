using DemoAPI.Infrastructure;
using DemoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudents _studentService;
        public StudentController(IStudents studentService)
        {
            _studentService = studentService;
        }


        [HttpPost("create_student")]
        public async Task<ApiResponse<bool>> AddStudent(StudentDTO input)
        {
           var result = await _studentService.CreateStudent(input);
            return result;
        }


        [HttpGet("all_students")]
        public async Task<ApiResponse<List<Student>>> GetStudents(int pageSize, int pageNumber)
        {
            var result = await _studentService.GetAllStudentsAsync(pageSize, pageNumber);
            return result;
        }



        [HttpPut("edit_data")]
        public async Task<bool> UpdateAsync(int id, StudentDTO input)
        {
            var result = await _studentService.UpdateAsync(id, input);
            return result;
        }


        [HttpDelete("item_delete")]
        public async Task<string> DeleteAsync(int id)
        {
            throw new Exception();
            var result = await _studentService.Deleter(id);
            return result;
        }
    }
}
