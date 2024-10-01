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
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherInterface _teacherRepository;
        private readonly IErrorMessageStrategy _errorMessageStrategy;

        public TeacherController(ITeacherInterface teacherRepository, IErrorMessageStrategy errorMessageStrategy)
        {
            _teacherRepository = teacherRepository;
            _errorMessageStrategy = errorMessageStrategy;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAll()
        {
            try
            {
                var teachers = await _teacherRepository.GetAllAsync();
                var (_, teacherDtos) = TeacherConventions.FromEntity(null!, teachers);

                return Ok(teacherDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetRetrieveErrorMessage(ConstantsValues.ObjectType.Teacher, ex.Message)
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDto>> GetById(int id)
        {
            try
            {
                var teacher = await _teacherRepository.GetByIdAsync(id);
                if (teacher == null)
                {
                    return NotFound();
                }

                var (teacherDto,_) = TeacherConventions.FromEntity(teacher, null!);

                return Ok(teacherDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetRetrieveErrorMessage(ConstantsValues.ObjectType.Teacher, ex.Message)
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Create(TeacherDto teacherDto)
        {
            try
            {
                var teacher = TeacherConventions.ToEntity(teacherDto);

                var response = await _teacherRepository.AddAsync(teacher);
                if (response.Flag)
                {
                    return CreatedAtAction(nameof(GetById), new { id = teacher.TeacherId }, response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetCreateErrorMessage(ConstantsValues.ObjectType.Teacher, ex.Message)
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> Update(int id, TeacherDto teacherDto)
        {
            try
            {
                if (id != teacherDto.TeacherId)
                {
                    return BadRequest("Teacher ID mismatch.");
                }

                var teacher = TeacherConventions.ToEntity(teacherDto);

                var response = await _teacherRepository.UpdateAsync(teacher);
                if (response.Flag)
                {
                    return NoContent();
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetUpdateErrorMessage(ConstantsValues.ObjectType.Teacher, ex.Message)
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> Delete(int id)
        {
            try
            {
                var response = await _teacherRepository.DeleteAsync(id);
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
                    _errorMessageStrategy.GetDeleteErrorMessage(ConstantsValues.ObjectType.Teacher, ex.Message)
                });
            }
        }

        [HttpGet("with-subjects")]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetTeachersWithSubjects()
        {
            try
            {
                var teachers = await _teacherRepository.GetTeachersWithSubjectsAsync();
                var (_, teacherDtos) = TeacherConventions.FromEntity(null, teachers);

                return Ok(teacherDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetRetrieveErrorMessage(ConstantsValues.ObjectType.Teacher, ex.Message)
                });
            }
        }

        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetTeachersByName(string name)
        {
            try
            {
                var teachers = await _teacherRepository.GetTeachersByNameAsync(name);
                var (_, teacherDtos) = TeacherConventions.FromEntity(null, teachers);

                return Ok(teacherDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetRetrieveErrorMessage(ConstantsValues.ObjectType.Teacher, ex.Message)
                });
            }
        }

        [HttpGet("exists/{id}")]
        public async Task<ActionResult<bool>> TeacherExists(int id)
        {
            try
            {
                var exists = await _teacherRepository.TeacherExistsAsync(id);
                return Ok(exists);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetCheckingErrorMessage(ConstantsValues.ObjectType.Teacher, ex.Message)
                });
            }
        }

        [HttpGet("{id}/with-subjects")]
        public async Task<ActionResult<TeacherDto>> GetTeacherWithSubjectsById(int id)
        {
            try
            {
                var teacher = await _teacherRepository.GetTeacherWithSubjectsByIdAsync(id);
                if (teacher == null)
                {
                    return NotFound();
                }

                var (teacherDto,_) = TeacherConventions.FromEntity(teacher, null!);

                return Ok(teacherDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message =
                    _errorMessageStrategy.GetRetrieveErrorMessage(ConstantsValues.ObjectType.Teacher, ex.Message)
                });
            }
        }
    }
}
