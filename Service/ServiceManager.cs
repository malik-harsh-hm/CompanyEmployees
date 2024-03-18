using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
        private readonly IAuthenticationService _authenticationService;
        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _companyService = new CompanyService(repositoryManager, logger, mapper);
            _employeeService = new EmployeeService(repositoryManager, logger, mapper);
            _authenticationService = new AuthenticationService(logger, mapper, userManager, configuration);
        }
        public ICompanyService CompanyService { get { return _companyService; } }
        public IEmployeeService EmployeeService { get { return _employeeService; } }
        public IAuthenticationService AuthenticationService { get { return _authenticationService; } }
    }
}
