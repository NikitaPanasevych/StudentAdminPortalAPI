using StudentAdminPortalAPI.Models;

namespace StudentAdminPortalAPI.Repositories
{
    public interface IStudentRepository
    {
        List<Student> GetStudents();
    }
}
