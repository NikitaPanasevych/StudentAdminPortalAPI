﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortalAPI.DomainModels;
using StudentAdminPortalAPI.Repositories;


namespace StudentAdminPortalAPI.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        public StudentController(IStudentRepository studentRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("Students")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await studentRepository.GetStudents();

            return Ok(mapper.Map<List<Student>>(students));
        }

        [HttpGet]
        [Route("Students/{studentId:guid}")]
        public async Task<IActionResult> GetStudent([FromRoute] Guid studentId)
        {
            var student = await studentRepository.GetStudent(studentId);
            if(student == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Student>(student));
        }
    }
}