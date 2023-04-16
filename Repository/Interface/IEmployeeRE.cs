using PruebaApiThales.Dto;
using PruebaApiThales.Entities;
using PruebaApiThales.Models;

namespace PruebaApiThales.Repository.Interface
{
    public interface IEmployeeRE
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeeAsync();

        Task<EmployeeDto> GetByIdEmployeeAsync(int idEmployee);

        Task<string> AddEmployee(EmployeeModel employee);
    }
}
