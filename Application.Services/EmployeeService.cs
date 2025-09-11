using Application.Services.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class EmployeeService(IEmployeeRepository EmployeeRepository) : IEmployeeService
{
    public bool AddEmployee(Employee employee)
    {
        return EmployeeRepository.Add(employee);
    }

    public bool DeleteEmployee(Guid employeeId)
    {
        return EmployeeRepository.Delete(employeeId);
    }

    public IEnumerable<Employee> GetAllEmployees()
    {
        return EmployeeRepository.GetAll();
    }

    public Employee? GetEmployeeById(Guid employeeId)
    {
        return EmployeeRepository.GetById(employeeId);
    }

    public bool UpdateEmployee(Employee employee)
    {
        return EmployeeRepository.Update(employee);
    }
}
