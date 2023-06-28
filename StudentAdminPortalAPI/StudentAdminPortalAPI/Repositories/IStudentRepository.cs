using StudentAdminPortalAPI.Models;

namespace StudentAdminPortalAPI.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudents();

        Task<Student> GetStudent(Guid studentId);
    }
}
