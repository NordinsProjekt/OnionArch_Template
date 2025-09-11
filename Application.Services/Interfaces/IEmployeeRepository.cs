using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces;

public interface IEmployeeRepository
{
    bool Add(Employee employee);
    bool Update(Employee employee);
    bool Delete(Guid id);
    Employee? GetById(Guid id);
    IEnumerable<Employee> GetAll();
}
