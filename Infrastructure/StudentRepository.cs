using DemoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI.Infrastructure
{
    public class StudentRepository : IStudents
    {
        private readonly ApplicationDBContext _dbContext;
        public StudentRepository(ApplicationDBContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task<ApiResponse<bool>> CreateStudent(StudentDTO input)
        {
            try
            {
                var student = new Student();
                student.Name = input.Name;
                student.Age = input.Age;
                student.Address = input.Address;
                await _dbContext.Students.AddAsync(student);
                await _dbContext.SaveChangesAsync();
                return new ApiResponse<bool>("Student created successfully", true);
            }
            catch (Exception ex)
            {
                var error = new List<string> { ex.Message };
               
                return new ApiResponse<bool>(error, "An error occured, contact admin");
            }
        }


        public async Task<ApiResponse<List<Student>>> GetAllStudentsAsync(int pageSize, int pageNumber)
        {
            try
            {
                var allStudents = await _dbContext.Students.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
                return new ApiResponse<List<Student>>("Request is successful", allStudents);

            }
            catch (Exception ex) {

                var error = new List<string> { ex.Message};
                return new ApiResponse<List<Student>>(error, "Could not fetch records. contact admin");
            }


        }


        public async Task<bool> UpdateAsync(int id, StudentDTO input)
        {
            var studentData = await _dbContext.Students.FindAsync(id);
            if(studentData == null)
            {
                return false;
            }

            studentData.Name = input.Name;
            studentData.Age = input.Age;
            studentData.Address = input.Address;

            await _dbContext.SaveChangesAsync();

            return true;

        }


        public async Task<string> Deleter(int id)
        {
            var student = await _dbContext.Students.FindAsync(id);
            if(student == null)
            {
                return "No student with this Id was found";
            }

            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
            return "student successfully removed";
        }

    }
}
