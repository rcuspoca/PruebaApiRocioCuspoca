using Microsoft.EntityFrameworkCore;
using PruebaApiThales.Dto;
using PruebaApiThales.Entities;
using PruebaApiThales.Exceptions;
using PruebaApiThales.Models;
using PruebaApiThales.Properties;
using PruebaApiThales.Repository.Interface;

namespace PruebaApiThales.Repository
{
    public class EmployeeRE : IEmployeeRE
    {
        private readonly ConexionBDContext _dbContext;
        private readonly string _parameter;

        public EmployeeRE(ConexionBDContext context, string configuration)
        {
            _dbContext = context;
            _parameter = configuration;
        }
        /// <summary>
        /// Obtain the list of employees
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<IEnumerable<EmployeeDto>> GetEmployeeAsync()
        {
            try
            {
                return await (from _employee in _dbContext.EmployeeEntity
                              orderby _employee.FirstName, _employee.SecondName, _employee.LastName
                              select new EmployeeDto()
                              {
                                  Id = _employee.Id,
                                  FirstName = _employee.FirstName,
                                  SecondName = _employee.SecondName,
                                  LastName = _employee.LastName,
                                  Address = _employee.Address,
                                  Salary = _employee.Salary,
                                  AnualSalary = _employee.Salary * int.Parse(_parameter)
                              }).ToListAsync();
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(Resource.ErrorGuardar, ex);
            }

        }

        /// <summary>
        /// Obtain employee by Id
        /// </summary>
        /// <param name="idEmployee"></param>
        /// <returns></returns>

        public async Task<EmployeeDto> GetByIdEmployeeAsync(int idEmployee)
        {
            try { 
                EmployeeDto? employeeData = await (from _employee in _dbContext.EmployeeEntity
                                                   where _employee.Id == idEmployee
                                                   select new EmployeeDto()
                                                   {
                                                       Id = _employee.Id,
                                                       FirstName = _employee.FirstName,
                                                       SecondName = _employee.SecondName,
                                                       LastName = _employee.LastName,
                                                       Address = _employee.Address,
                                                       Salary = _employee.Salary,
                                                       AnualSalary = _employee.Salary * int.Parse(_parameter)
                                                   }).FirstOrDefaultAsync();
                return employeeData;
            }
             catch (ValidationException ex)
            {
                throw new ValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(Resource.ErrorGuardar, ex);
            }
        }

        /// <summary>
        /// Save an employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// <exception cref="ValidationException"></exception>
        /// <exception cref="Exception"></exception>

        public async Task<string> AddEmployee(EmployeeModel employee)
        {
            try
            {
                if (await _dbContext.EmployeeEntity.AnyAsync(x => x.FirstName.Equals(employee.FirstName)))
                {                   
                    throw new ValidationException(Resource.ValidacionItemExistente.Replace("{0}", employee.FirstName));
                }
                else
                {
                    var employeeRegistrado = _dbContext.Add(BuildEmployeeEntity(employee));
                    _ = _dbContext.SaveChanges();

                    employee.Id = employeeRegistrado.Entity.Id;
                }
                return string.Empty;                
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(Resource.ErrorGuardar, ex);
            }
        }

        private static EmployeeEntity BuildEmployeeEntity(EmployeeModel employee)
        {
            return new EmployeeEntity()
            {
                Id = employee.Id,
                FirstName = employee.FirstName.Trim().ToUpper(),
                SecondName = employee.SecondName.Trim().ToUpper(),
                LastName = employee.LastName.Trim().ToUpper(),
                Address = employee.Address,
                Salary = employee.Salary               
            };
        }
    }
}
