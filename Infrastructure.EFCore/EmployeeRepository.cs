using Application.Services.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EFCore;

public class EmployeeRepository : IEmployeeRepository
{
    private List<Employee> _employees = new();
    
    public EmployeeRepository()
    {
        // Seed with 10 test employees
        SeedData();

    }
    public bool Add(Employee employee)
    {
        _employees.Add(employee);
        return true;
    }

    public bool Delete(Guid id)
    {
        var employee = _employees.FirstOrDefault(e => e.Id == id);
        if (employee is null) return false;

        _employees.Remove(employee);
        return true;
    }

    public Employee? GetById(Guid id)
    {
        return _employees.FirstOrDefault(e => e.Id == id);
    }

    public bool Update(Employee employee)
    {
        var existingEmployee = _employees.FirstOrDefault(e => e.Id == employee.Id);
        if (existingEmployee is null) return false;

        existingEmployee.EmployeeNumber = employee.EmployeeNumber;
        existingEmployee.FirstName = employee.FirstName;
        existingEmployee.LastName = employee.LastName;
        existingEmployee.Email = employee.Email;

        return true;
    }

    public IEnumerable<Employee> GetAll()
    {
        return _employees.ToList();
    }

    private void SeedData()
    {
        // This method can be used to seed initial data if needed
        _employees.Clear();
        _employees.AddRange(new List<Employee>
        {
            new Employee { Id = Guid.NewGuid(), EmployeeNumber = 1001, FirstName = "John", LastName = "Doe", Email = "john.doe@company.com" },
            new Employee { Id = Guid.NewGuid(), EmployeeNumber = 1002, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@company.com" },
            new Employee { Id = Guid.NewGuid(), EmployeeNumber = 1003, FirstName = "Michael", LastName = "Johnson", Email = "michael.johnson@company.com" },
            new Employee { Id = Guid.NewGuid(), EmployeeNumber = 1004, FirstName = "Emily", LastName = "Davis", Email = "emily.davis@company.com" },
            new Employee { Id = Guid.NewGuid(), EmployeeNumber = 1005, FirstName = "David", LastName = "Wilson", Email = "david.wilson@company.com" },
            new Employee { Id = Guid.NewGuid(), EmployeeNumber = 1006, FirstName = "Sarah", LastName = "Brown", Email = "sarah.brown@company.com" },
            new Employee { Id = Guid.NewGuid(), EmployeeNumber = 1007, FirstName = "Robert", LastName = "Taylor", Email = "robert.taylor@company.com" },
            new Employee { Id = Guid.NewGuid(), EmployeeNumber = 1008, FirstName = "Lisa", LastName = "Anderson", Email = "lisa.anderson@company.com" },
            new Employee { Id = Guid.NewGuid(), EmployeeNumber = 1009, FirstName = "James", LastName = "Thomas", Email = "james.thomas@company.com" },
            new Employee { Id = Guid.NewGuid(), EmployeeNumber = 1010, FirstName = "Amanda", LastName = "Martinez", Email = "amanda.martinez@company.com" }
        });
    }
}
