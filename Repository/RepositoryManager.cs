using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    // Unit of Work
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly ICompanyRepository _companyRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _companyRepository = new CompanyRepository(context);
            _employeeRepository = new EmployeeRepository(context);
        }
        public ICompanyRepository Company {  get { return _companyRepository; } }

        public IEmployeeRepository Employee { get { return _employeeRepository; } }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
