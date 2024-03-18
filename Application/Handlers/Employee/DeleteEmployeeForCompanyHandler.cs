using Application.Commands.Company;
using Application.Commands.Employee;
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
    internal sealed class DeleteEmployeeForCompanyHandler : IRequestHandler<DeleteEmployeeForCompanyCommand>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public DeleteEmployeeForCompanyHandler(IRepositoryManager repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task Handle(DeleteEmployeeForCompanyCommand request, CancellationToken cancellationToken)
        {
            var companyEntity = await _repository.Company.GetCompany(request.CompanyId, request.TrackChanges);
            if (companyEntity is null)
                throw new CompanyNotFoundException(request.CompanyId);

            var employeeEntity = await _repository.Employee.GetEmployee(request.CompanyId, request.EmployeeId, request.TrackChanges);
            if (employeeEntity is null)
                throw new EmployeeNotFoundException(request.CompanyId);

            _repository.Employee.DeleteEmployee(employeeEntity);
            await _repository.SaveAsync();
        }
    }
}
