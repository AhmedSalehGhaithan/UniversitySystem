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
    public class MajorController : ControllerBase
    {
        private readonly IMajorInterface _majorRepository;
        private readonly IErrorMessageStrategy _errorMessageStrategy;


        public MajorController(IMajorInterface majorRepository, IErrorMessageStrategy errorMessageStrategy)
        {
            _majorRepository = majorRepository;
            _errorMessageStrategy = errorMessageStrategy;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MajorDto>>> GetAll()
        {
            try
            {
                var majors = await _majorRepository.GetAllAsync();
                var (_,majorDto) = MajorConventions.FromEntity(null!, majors);
                return Ok(majorDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetRetrieveErrorMessage(ConstantsValues.ObjectType.Major, ex.Message)
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MajorDto>> GetById(int id)
        {
            try
            {
                var major = await _majorRepository.GetByIdAsync(id);
                if (major == null)
                {
                    return NotFound();
                }

                var (majorDto, _) = MajorConventions.FromEntity(major, null!);

                return Ok(majorDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message =
                    _errorMessageStrategy.GetRetrieveErrorMessage(ConstantsValues.ObjectType.Major, ex.Message)
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Create(MajorDto majorDto)
        {
            try
            {
                var major = MajorConventions.ToEntity(majorDto);
               
                var response = await _majorRepository.AddAsync(major);
                if (response.Flag)
                {
                    return CreatedAtAction(nameof(GetById), new { id = major.MajorId }, response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetCreateErrorMessage(ConstantsValues.ObjectType.Major, ex.Message)
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> Update(int id, MajorDto majorDto)
        {
            try
            {
                if (id != majorDto.MajorId)
                {
                    return BadRequest("Major ID mismatch.");
                }

                var major = MajorConventions.ToEntity(majorDto);
                

                var response = await _majorRepository.UpdateAsync(major);
                if (response.Flag)
                {
                    return NotFound(response.Message);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Flag = false,
                    Message = 
                    _errorMessageStrategy.GetUpdateErrorMessage(ConstantsValues.ObjectType.Major, ex.Message)
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> Delete(int id)
        {
            try
            {
                var response = await _majorRepository.DeleteAsync(id);
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
                    _errorMessageStrategy.GetDeleteErrorMessage(ConstantsValues.ObjectType.Major, ex.Message)
                });
            }
        }

    }
}
