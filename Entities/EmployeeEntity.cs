using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaApiThales.Entities
{
    [Table("Employee")]
    public class EmployeeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("FirstName")]
        public string? FirstName { get; set; }

        [Column("SecondName")]
        public string? SecondName { get; set; }

        [Column("LastName")]
        public string? LastName { get; set; }

        [Column("Address")]
        public string? Address { get; set; }

        [Column("Salary")]
        public decimal Salary { get; set; }       
    }
}
