using DemoAPI.Models;

namespace DemoAPI.Infrastructure
{
    public interface IStudents
    {
        Task<bool> CreateStudent(StudentDTO input);

        Task<List<Student>> GetAllStudentsAsync(int pageSize, int pageNumber);


        Task<bool> UpdateAsync(int Id, StudentDTO data);

        Task<string> Deleter(int Id);
    }
}
