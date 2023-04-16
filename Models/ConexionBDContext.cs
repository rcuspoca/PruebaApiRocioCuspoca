using Microsoft.EntityFrameworkCore;
using PruebaApiThales.Entities;

namespace PruebaApiThales.Models
{
    public class ConexionBDContext : DbContext
    {
        public ConexionBDContext(DbContextOptions<ConexionBDContext> options) : base(options) { }

        public DbSet<EmployeeEntity> EmployeeEntity { get; set; }
    }
}