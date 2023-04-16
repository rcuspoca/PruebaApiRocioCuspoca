namespace PruebaApiThales.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string?SecondName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public decimal AnualSalary { get; set; }
    }
}

