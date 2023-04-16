using Microsoft.AspNetCore.Mvc;
using PruebaApiThales.Business.Interface;
using PruebaApiThales.Dto;
using PruebaApiThales.Exceptions;
using PruebaApiThales.Models;
using PruebaApiThales.Properties;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaApiThales.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeBL _employeeBusiness;     

        public EmployeeController(IEmployeeBL employeeBusiness)
        {
            _employeeBusiness = employeeBusiness;
        }

        /// <summary>
        /// Gets all employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get()
        {
            try
            {
                return Ok(await _employeeBusiness.GetEmployeeAsync());
            }
            catch (ValidationException be)
            {
                return StatusCode(StatusCodes.Status400BadRequest, be.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.StackTrace);
            }
        }

        /// <summary>
        /// Obtains employee by IdEmployee
        /// </summary>
        /// <returns></returns>

        [HttpGet("{idEmployee}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployeeDto>> GetByIdEmployee(int idEmployee)
        {
            try
            {
                return Ok(await _employeeBusiness.GetByIdEmployeeAsync(idEmployee));
            }
            catch (ValidationException be)
            {
                return StatusCode(StatusCodes.Status400BadRequest, be.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.StackTrace);
            }
        }
        /// <summary>
        /// Add an employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> Post([FromBody] EmployeeModel employee)
        {
            try
            {
                _ = await _employeeBusiness.AddEmployee(employee);
                return StatusCode(StatusCodes.Status201Created, string.Format(Resource.ElementoCreadoSatisfactoriamente, employee.FirstName));
            }
            catch (ValidationException be)
            {
                return StatusCode(StatusCodes.Status400BadRequest, be.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, string.IsNullOrEmpty(ex.Message) ? Resource.ErrorInternoServidor : ex.Message);
            }
        }
    }
}
