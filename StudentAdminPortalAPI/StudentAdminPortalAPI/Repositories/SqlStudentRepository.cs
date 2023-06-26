using StudentAdminPortalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentAdminPortalAPI.Repositories
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly StudentAdminContext context;
        public SqlStudentRepository(StudentAdminContext context)
        {
            this.context = context;
        }
        public async Task<List<Student>> GetStudents()
        {
            return await context.Student.Include(nameof(Gender))
                                  .Include(nameof(Address))
                                  .ToListAsync();
        }
    }
}
