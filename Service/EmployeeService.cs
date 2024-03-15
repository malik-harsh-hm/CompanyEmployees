using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges)
        {
            var companyEntity = _repository.Company.GetCompany(companyId, trackChanges); 
            if (companyEntity is null) 
                throw new CompanyNotFoundException(companyId);

            var employeeEntities = _repository.Employee.GetEmployees(companyId, trackChanges);

            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeeEntities);

            return employeesDto;
        }

        public EmployeeDto GetEmployee(Guid companyId, Guid employeeId, bool trackChanges)
        {
            var companyEntity = _repository.Company.GetCompany(companyId, trackChanges);
            if (companyEntity is null)
                throw new CompanyNotFoundException(companyId);

            var employeeEntity = _repository.Employee.GetEmployee(companyId, employeeId, trackChanges);
            if (employeeEntity is null)
                throw new EmployeeNotFoundException(companyId);

            var employeeDto = _mapper.Map<EmployeeDto>(employeeEntity);

            return employeeDto;
        }

        public EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto employeeForCreation, bool trackChanges)
        {
            var companyEntity = _repository.Company.GetCompany(companyId, trackChanges); 
            if (companyEntity is null) 
                throw new CompanyNotFoundException(companyId); 
            
            var employeeEntity = _mapper.Map<Employee>(employeeForCreation); 

            _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity); 
            _repository.Save();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return employeeToReturn;
        }

        public void DeleteEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges)
        {
            var companyEntity = _repository.Company.GetCompany(companyId, trackChanges);
            if (companyEntity is null)
                throw new CompanyNotFoundException(companyId);

            var employeeEntity = _repository.Employee.GetEmployee(companyId, employeeId, trackChanges);
            if (employeeEntity is null)
                throw new EmployeeNotFoundException(companyId);

            _repository.Employee.DeleteEmployee(employeeEntity); 
            _repository.Save();
        }

        public void UpdateEmployeeForCompany(Guid companyId, Guid employeeId, EmployeeForUpdationDto employeeForUpdation, bool compTrackChanges, bool empTrackChanges)
        {
            var companyEntity = _repository.Company.GetCompany(companyId, compTrackChanges);
            if (companyEntity is null)
                throw new CompanyNotFoundException(companyId);

            var employeeEntity = _repository.Employee.GetEmployee(companyId, employeeId, empTrackChanges);
            if (employeeEntity is null)
                throw new EmployeeNotFoundException(companyId);

            // Connected Update:
            // We are mapping from the employeeForUpdate object (we will change just the age property in a request)
            // to the employeeEntity — thus changing the state of the employeeEntity object to Modified.
            _mapper.Map(employeeForUpdation, employeeEntity); 
            _repository.Save();
        }
    }
}