using AutoMapper;
using Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly ICompanyService _companyService;
        private readonly IEmployeeService _employeeService;
        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _companyService = new CompanyService(repositoryManager, logger, mapper);
            _employeeService = new EmployeeService(repositoryManager, logger, mapper);
        }
        public ICompanyService CompanyService { get { return _companyService; } }
        public IEmployeeService EmployeeService { get { return _employeeService; } }
    }
}
