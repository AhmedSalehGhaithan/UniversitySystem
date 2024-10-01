using Microsoft.AspNetCore.Mvc;
using University.Application.DTOs;
using University.Application.DTOs.ConstantsValues;
using University.Application.DTOs.Conventions;
using University.Application.Interfaces;
using University.Application.Interfaces.StrategyInterfaces;
using University.Application.Responses;

namespace University.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsInterface _studentsRepository;
        private readonly IErrorMessageStrategy _errorMessageStrategy;


        public StudentsController(IStudentsInterface studentsRepository, IErrorMessageStrategy errorMessageStrategy)
        {
            _studentsRepository = studentsRepository;
            _errorMessageStrategy = errorMessageStrategy;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll()
        {
            try
            {
                var students = await _studentsRepository.GetAllAsync();
                var (_, studentDtos) = StudentConventions.FromEntity(null!, students);

                return Ok(studentDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetRetrieveErrorMessage(ConstantsValues.ObjectType.Student, ex.Message)
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetById(int id)
        {
            try
            {
                var student = await _studentsRepository.GetByIdAsync(id);
                if (student == null)
                {
                    return NotFound();
                }

                var (studentDto,_) = StudentConventions.FromEntity(student, null); 

                return Ok(studentDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetRetrieveErrorMessage(ConstantsValues.ObjectType.Student, ex.Message)
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Create(StudentDto studentDto)
        {
            try
            {
                var student = StudentConventions.ToEntity(studentDto);
                var response = await _studentsRepository.AddAsync(student);
                if (response.Flag)
                {
                    return CreatedAtAction(nameof(GetById), new { id = student.StudentId }, response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetCreateErrorMessage(ConstantsValues.ObjectType.Student, ex.Message)
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> Update(int id, StudentDto studentDto)
        {
            try
            {
                if (id != studentDto.StudentId)
                {
                    return BadRequest("Student ID mismatch.");
                }

                var student = StudentConventions.ToEntity(studentDto);

                var response = await _studentsRepository.UpdateAsync(student);
                if (response.Flag)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetUpdateErrorMessage(ConstantsValues.ObjectType.Student, ex.Message)
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> Delete(int id)
        {
            try
            {
                var response = await _studentsRepository.DeleteAsync(id);
                if (response.Flag)
                {
                    return NoContent();
                }
                return NotFound(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetDeleteErrorMessage(ConstantsValues.ObjectType.Student, ex.Message)
                });
            }
        }

        [HttpGet("by-major/{majorId}")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudentsByMajorId(int majorId)
        {
            try
            {
                var students = await _studentsRepository.GetStudentsByMajorIdAsync(majorId);
                var (_, studentDtos) = StudentConventions.FromEntity(null!, students);

                return Ok(studentDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetRetrieveErrorMessage(ConstantsValues.ObjectType.Student, ex.Message)
                });
            }
        }       

        [HttpGet("{id}/with-major")]
        public async Task<ActionResult<StudentDto>> GetStudentWithMajorById(int id)
        {
            try
            {
                var student = await _studentsRepository.GetStudentWithMajorByIdAsync(id);
                if (student == null)
                {
                    return NotFound();
                }

                var (studentDto,_) = StudentConventions.FromEntity(student, null!);

                return Ok(studentDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetRetrieveErrorMessage(ConstantsValues.ObjectType.Student, ex.Message)
                });
            }
        }

        [HttpGet("exists/{id}")]
        public async Task<ActionResult<bool>> StudentExists(int id)
        {
            try
            {
                var exists = await _studentsRepository.StudentExistsAsync(id);
                return Ok(exists);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetCheckingErrorMessage(ConstantsValues.ObjectType.Student, ex.Message)
                });
            }
        }

        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudentsByName(string name)
        {
            try
            {
                var students = await _studentsRepository.GetStudentsByNameAsync(name);
                var (_, studentDtos) = StudentConventions.FromEntity(null!, students);

                return Ok(studentDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetRetrieveErrorMessage(ConstantsValues.ObjectType.Student, ex.Message)
                });
            }
        }
    }
}
