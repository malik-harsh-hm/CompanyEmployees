using Contracts;
using Entities.Models;
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
        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges)
        {
            var employees = FindByCondition(e => e.CompanyId == companyId, trackChanges)
                .OrderBy(c => c.Name)
                .ToList();

            return employees;
        }

        public Employee GetEmployee(Guid companyId, Guid employeeId, bool trackChanges)
        {
            var employee = FindByCondition(e => e.CompanyId == companyId && e.Id == employeeId, trackChanges)
                .SingleOrDefault();
            return employee;
        }
    }
}