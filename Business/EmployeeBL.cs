using PruebaApiThales.Business.Interface;
using PruebaApiThales.Dto;
using PruebaApiThales.Entities;
using PruebaApiThales.Models;
using PruebaApiThales.Repository.Interface;

namespace PruebaApiThales.Business
{
    public class EmployeeBL : IEmployeeBL
    {
        private readonly IEmployeeRE _employeeRepo;
        public EmployeeBL(IEmployeeRE employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeeAsync() => await _employeeRepo.GetEmployeeAsync();

        public async Task<EmployeeDto> GetByIdEmployeeAsync(int idEmployee) => await _employeeRepo.GetByIdEmployeeAsync(idEmployee);

        public async Task<string> AddEmployee(EmployeeModel employee) => await _employeeRepo.AddEmployee(employee);
    }
}