using Application.Commands.Company;
using Application.Commands.Employee;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Employee
{
    internal sealed class UpdateEmployeeForCompanyHandler : IRequestHandler<UpdateEmployeeForCompanyCommand>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public UpdateEmployeeForCompanyHandler(IRepositoryManager repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task Handle(UpdateEmployeeForCompanyCommand request, CancellationToken cancellationToken)
        {
            var companyEntity = await _repository.Company.GetCompany(request.CompanyId, request.CompTrackChanges);
            if (companyEntity is null)
                throw new CompanyNotFoundException(request.CompanyId);

            var employeeEntity = await _repository.Employee.GetEmployee(request.CompanyId, request.EmployeeId, request.EmpTrackChanges);
            if (employeeEntity is null)
                throw new EmployeeNotFoundException(request.CompanyId);

            // Connected Update:
            // We are mapping from the employeeForUpdate object (we will change just the age property in a request)
            // to the employeeEntity — thus changing the state of the employeeEntity object to Modified.
            _mapper.Map(request.EmployeeForUpdation, employeeEntity);
            await _repository.SaveAsync();
        }
    }
}
