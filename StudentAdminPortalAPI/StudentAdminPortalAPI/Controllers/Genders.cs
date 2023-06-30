using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortalAPI.DomainModels;
using StudentAdminPortalAPI.Repositories;

namespace StudentAdminPortalAPI.Controllers
{
    [ApiController]
    public class Genders : Controller
    {
        private IStudentRepository studentRepository;
        private readonly IMapper Mapper;
        public Genders(IStudentRepository studentRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            Mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllGenders()
        {
            var genderList = await studentRepository.GetAllGEnders();
            if(genderList == null || !genderList.Any())
            {
                return NotFound();
            }

            return Ok(Mapper.Map<List<Gender>>(genderList));
        }
    }
}
