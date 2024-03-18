using MediatR;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Employee
{
    public sealed class CreateEmployeeForCompanyCommand : IRequest<EmployeeDto>
    {
        public Guid CompanyId { get; }
        public EmployeeForCreationDto EmployeeForCreation { get; }
        public bool TrackChanges { get; }

        public CreateEmployeeForCompanyCommand(Guid companyId, EmployeeForCreationDto employeeForCreation, bool trackChanges)
        {
            CompanyId = companyId;
            EmployeeForCreation = employeeForCreation;
            TrackChanges = trackChanges;
        }
    }
}
