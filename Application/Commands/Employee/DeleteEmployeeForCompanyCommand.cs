using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Employee
{
    public sealed class DeleteEmployeeForCompanyCommand : IRequest
    {
        public Guid CompanyId { get; }
        public Guid EmployeeId { get; }
        public bool TrackChanges { get; }

        public DeleteEmployeeForCompanyCommand(Guid companyId, Guid employeeId, bool trackChanges)
        {
            CompanyId = companyId;
            EmployeeId = employeeId;
            TrackChanges = trackChanges;
        }
    }
}
