using Application.Queries.Company;
using Application.Queries.Employee;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using MediatR;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Employee
{
    internal sealed class GetEmployeeHandler : IRequestHandler<GetEmployeeQuery, EmployeeDto>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public GetEmployeeHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var companyEntity = await _repository.Company.GetCompany(request.CompanyId, request.TrackChanges);
            if (companyEntity is null)
                throw new CompanyNotFoundException(request.CompanyId);

            var employeeEntity = await _repository.Employee.GetEmployee(request.CompanyId, request.EmployeeId, request.TrackChanges);
            if (employeeEntity is null)
                throw new EmployeeNotFoundException(request.CompanyId);

            var employeeDto = _mapper.Map<EmployeeDto>(employeeEntity);

            return employeeDto;
        }
    }
}
