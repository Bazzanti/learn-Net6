namespace FormazioneNET6.Services
{
    public interface EmployeeInterface
    {
        Task<List<Employee>> GetListAsync();
        Task<Employee> FindAsync(int id);
        Task<Employee> Add(Employee employee);
        Task<Employee> Update(Employee employee);
        Task<Boolean> Delete(int id);

    }
}
