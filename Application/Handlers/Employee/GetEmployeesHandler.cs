using Application.Queries.Company;
using Application.Queries.Employee;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Employee
{
    internal sealed class GetEmployeesHandler : IRequestHandler<GetEmployeesQuery, IEnumerable<EmployeeDto>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public GetEmployeesHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var companyEntity = await _repository.Company.GetCompany(request.CompanyId, request.TrackChanges);
            if (companyEntity is null)
                throw new CompanyNotFoundException(request.CompanyId);

            var employeeEntities = await _repository.Employee.GetEmployees(request.CompanyId, request.EmployeeParameters, request.TrackChanges);

            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeeEntities);

            return employeesDto;
        }
    }
}
