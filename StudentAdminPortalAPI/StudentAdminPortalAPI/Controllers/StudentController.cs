using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortalAPI.DomainModels;
using StudentAdminPortalAPI.Repositories;
using DataModels = StudentAdminPortalAPI.Models;
using Microsoft.AspNetCore.Http;


namespace StudentAdminPortalAPI.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        public StudentController(
            IStudentRepository studentRepository, 
            IMapper mapper, 
            IImageRepository imageRepository
           )
            
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
            this.imageRepository = imageRepository;
        }

        [HttpGet]
        [Route("Students")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await studentRepository.GetStudents();

            return Ok(mapper.Map<List<Student>>(students));
        }

        [HttpGet]
        [Route("Students/{studentId:guid}"), ActionName("GetStudent")]
        public async Task<IActionResult> GetStudent([FromRoute] Guid studentId)
        {
            var student = await studentRepository.GetStudent(studentId);
            if(student == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Student>(student));
        }

        [HttpPut]
        [Route("Students/{studentId:guid}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] Guid studentId, [FromBody] UpdateStudentRequest request)
        {
            if(await studentRepository.Exists(studentId)){
                //Update details
                var updateStudent = await studentRepository.UpdateStudent(studentId, mapper.Map<DataModels.Student>(request));
                if(updateStudent != null)
                {
                    return Ok(mapper.Map<Student>(updateStudent));
                }

            }
                return NotFound(); 
        }

        [HttpDelete]
        [Route("Students/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] Guid studentId)
        {
            if (await studentRepository.Exists(studentId))
            {
                //Delete student
                var student = await studentRepository.DeleteStudent(studentId);
                return Ok(mapper.Map<Student>(student));
            }
            return NotFound();
        }

        [HttpPost]
        [Route("Students/Add")]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentRequest request)
        {
            var student = await studentRepository.AddStudent(mapper.Map<DataModels.Student>(request));
            return CreatedAtAction(nameof(GetStudent), new { studentId = student.Id },
                mapper.Map<Student>(student));
        }

        [HttpPost]
        [Route("Students/{studentId:guid}/upload-image")]
        public async Task<IActionResult> UploadImage([FromRoute] Guid studentId, IFormFile profileImage)
        {
            // Check if student exists
            if (await studentRepository.Exists(studentId))
            { 
                var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);

                var fileImagePath = await imageRepository.Upload(profileImage, fileName);

                if(await studentRepository.UpdateProfileImage(studentId, fileImagePath))
                {
                    return Ok(fileImagePath);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured while updating image");
            }
            return NotFound();
        }
    }
}