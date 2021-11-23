using Microsoft.EntityFrameworkCore;

namespace FormazioneNET6.Services
{
    public class EmployeeService: EmployeeInterface
    {
        EmployeeContext db;

        public EmployeeService(EmployeeContext db)
        {
            this.db = db;
        }

        public async Task<List<Employee>> GetListAsync() => await db.Employees.ToListAsync();

        public async Task<Employee> FindAsync(int id) => await db.Employees.FindAsync(id);

        public async Task<Employee> Add(Employee employee)
        {
            await db.Employees.AddAsync(employee);
            await db.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> Update(Employee employee)
        {
            var existingEmployee = await db.Employees.FindAsync(employee.Id);
            if (existingEmployee == null) return null;

            existingEmployee.Name = employee.Name;
            existingEmployee.Surname = employee.Surname;
            existingEmployee.Code = employee.Code;
            existingEmployee.Ufficio = employee.Ufficio;

            db.Update(existingEmployee);
            await db.SaveChangesAsync();

            return existingEmployee;
        }

        public async Task<Boolean> Delete(int id)
        {
            var employee = await db.Employees.FindAsync(id);
            if (employee == null) return false;

            db.Employees.Remove(employee);
            await db.SaveChangesAsync();

            return true;
        }
    }
}
