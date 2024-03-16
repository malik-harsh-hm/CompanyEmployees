using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    // Concrete repository
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Employee>> GetEmployees(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
        {
            var employees = await FindByCondition(e => e.CompanyId == companyId, trackChanges)
                .OrderBy(c => c.Name)
                .FilterEmployees(employeeParameters?.MinAge, employeeParameters?.MaxAge)
                .SearchEmployees(employeeParameters?.SearchTerm)
                .PageEmployees(employeeParameters?.PageNumber, employeeParameters?.PageSize)
                .ToListAsync();

            return employees;
        }

        public async Task<Employee> GetEmployee(Guid companyId, Guid employeeId, bool trackChanges)
        {
            var employee = await FindByCondition(e => e.CompanyId == companyId && e.Id == employeeId, trackChanges)
                .SingleOrDefaultAsync();

            return employee;
        }

        // Kept synchronous as we are not makinh any changes to DB
        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }
    }
}