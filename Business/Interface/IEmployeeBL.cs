using PruebaApiThales.Dto;
using PruebaApiThales.Entities;
using PruebaApiThales.Models;

namespace PruebaApiThales.Business.Interface
{
    public interface IEmployeeBL
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeeAsync();

        Task<EmployeeDto> GetByIdEmployeeAsync(int idEmployee);

        Task<string> AddEmployee(EmployeeModel producto);
    }
}