using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces;

public interface IEmployeeService
{
    bool AddEmployee(Employee employee);
    bool DeleteEmployee(Guid employeeId);
    bool UpdateEmployee(Employee employee);
    Employee? GetEmployeeById(Guid employeeId);
    IEnumerable<Employee> GetAllEmployees();
}
