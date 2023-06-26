using StudentAdminPortalAPI.Models;

namespace StudentAdminPortalAPI.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudents();
    }
}
