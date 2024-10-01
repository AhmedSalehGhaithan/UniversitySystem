using Microsoft.AspNetCore.Mvc;
using University.Application.DTOs;
using University.Application.DTOs.ConstantsValues;
using University.Application.DTOs.Conventions;
using University.Application.Interfaces;
using University.Application.Interfaces.StrategyInterfaces;
using University.Application.Responses;
using University.Domain.Entities;

namespace University.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectInterface _subjectRepository;
        private readonly IErrorMessageStrategy _errorMessageStrategy;

        public SubjectController(ISubjectInterface subjectRepository, IErrorMessageStrategy errorMessageStrategy)
        {
            _subjectRepository = subjectRepository;
           _errorMessageStrategy = errorMessageStrategy;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetAll()
        {
            try
            {
                var subjects = await _subjectRepository.GetAllAsync();
                var (_, subjectDtos) = SubjectConventions.FromEntity(null!, subjects);

                return Ok(subjectDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetRetrieveErrorMessage(ConstantsValues.ObjectType.Subject, ex.Message)
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDto>> GetById(int id)
        {
            try
            {
                var subject = await _subjectRepository.GetByIdAsync(id);
                if (subject == null)
                {
                    return NotFound();
                }

                var (subjectDto,_) =  SubjectConventions.FromEntity(subject, null!);

                return Ok(subjectDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message =
                    _errorMessageStrategy.GetRetrieveErrorMessage(ConstantsValues.ObjectType.Subject, ex.Message)
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Create(SubjectDto subjectDto)
        {
            try
            {
                var subject = SubjectConventions.ToEntity(subjectDto);

                var response = await _subjectRepository.AddAsync(subject);
                if (response.Flag)
                {
                    return CreatedAtAction(nameof(GetById), new { id = subject.SubjectId }, response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetCreateErrorMessage(ConstantsValues.ObjectType.Subject, ex.Message)
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> Update(int id, SubjectDto subjectDto)
        {
            try
            {
                if (id != subjectDto.SubjectId)
                {
                    return BadRequest("Subject ID mismatch.");
                }

                var subject = SubjectConventions.ToEntity(subjectDto);

                var response = await _subjectRepository.UpdateAsync(subject);
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
                    _errorMessageStrategy.GetUpdateErrorMessage(ConstantsValues.ObjectType.Subject, ex.Message)
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> Delete(int id)
        {
            try
            {
                var response = await _subjectRepository.DeleteAsync(id);
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
                    _errorMessageStrategy.GetDeleteErrorMessage(ConstantsValues.ObjectType.Subject, ex.Message)
                });
            }
        }

        [HttpGet("by-major/{majorId}")]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetSubjectsByMajorId(int majorId)
        {
            try
            {
                var subjects = await _subjectRepository.GetSubjectsByMajorIdAsync(majorId);
                var (_, subjectDtos) = SubjectConventions.FromEntity(null,subjects);

                return Ok(subjectDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetRetrieveErrorMessage(ConstantsValues.ObjectType.Subject, ex.Message)
                });
            }
        }

        [HttpGet("by-teacher/{teacherId}")]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetSubjectsByTeacherId(int teacherId)
        {
            try
            {
                var subjects = await _subjectRepository.GetSubjectsByTeacherIdAsync(teacherId);
                var (_, subjectDtos) = SubjectConventions.FromEntity(null, subjects);

                return Ok(subjectDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetRetrieveErrorMessage(ConstantsValues.ObjectType.Subject, ex.Message)
                });
            }
        }

        [HttpGet("{id}/with-teacher")]
        public async Task<ActionResult<SubjectDto>> GetSubjectWithTeacherById(int id)
        {
            try
            {
                var subject = await _subjectRepository.GetSubjectWithTeacherByIdAsync(id);
                if (subject == null)
                {
                    return NotFound();
                }

                var (subjectDto,_) = SubjectConventions.FromEntity(subject, null);

                return Ok(subjectDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetRetrieveErrorMessage(ConstantsValues.ObjectType.Subject, ex.Message)
                });
            }
        }

        [HttpGet("exists/{id}")]
        public async Task<ActionResult<bool>> SubjectExists(int id)
        {
            try
            {
                var exists = await _subjectRepository.SubjectExistsAsync(id);
                return Ok(exists);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetCheckingErrorMessage(ConstantsValues.ObjectType.Subject, ex.Message)
                });
            }
        }
    }
}
