using MediatR;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Employee
{
    public sealed class GetEmployeeQuery : IRequest<EmployeeDto>
    {
        public Guid CompanyId { get; }
        public Guid EmployeeId { get; }
        public bool TrackChanges { get; }

        public GetEmployeeQuery(Guid companyId, Guid employeeId, bool trackChanges)
        {
            CompanyId = companyId;
            EmployeeId = employeeId;
            TrackChanges = trackChanges;
        }
    }
}
