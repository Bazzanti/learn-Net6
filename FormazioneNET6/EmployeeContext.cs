using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormazioneNET6
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options):base(options)
        {
        
        }

        public DbSet<Employee> Employees { get; set; }
    }

    [Table("TB_EMPLOYEE", Schema = "dbo")]
    public class Employee
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Ufficio { get; set; }

    }
}
