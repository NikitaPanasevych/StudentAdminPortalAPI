using StudentAdminPortalAPI.Models;

namespace StudentAdminPortalAPI.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudents();

        Task<Student> GetStudent(Guid studentId);

        Task<List<Gender>> GetAllGEnders();

        Task<bool> Exists(Guid studentId);

        Task<Student> UpdateStudent(Guid studentId, Student request);

        Task<Student> DeleteStudent(Guid studentId);

        Task<Student> AddStudent(Student request);
    }
}
