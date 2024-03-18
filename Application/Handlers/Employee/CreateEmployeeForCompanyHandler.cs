using Application.Commands.Company;
using Application.Commands.Employee;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Employee
{
    internal sealed class CreateEmployeeForCompanyHandler : IRequestHandler<CreateEmployeeForCompanyCommand, EmployeeDto>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public CreateEmployeeForCompanyHandler(IRepositoryManager repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        public async Task<EmployeeDto> Handle(CreateEmployeeForCompanyCommand request, CancellationToken cancellationToken)
        {
            var companyEntity = await _repository.Company.GetCompany(request.CompanyId, request.TrackChanges);
            if (companyEntity is null)
                throw new CompanyNotFoundException(request.CompanyId);

            var employeeEntity = _mapper.Map<Entities.Models.Employee>(request.EmployeeForCreation);

            _repository.Employee.CreateEmployeeForCompany(request.CompanyId, employeeEntity);
            await _repository.SaveAsync();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return employeeToReturn;
        }
    }
}
