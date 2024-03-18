using MediatR;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Employee
{
    public sealed class GetEmployeesQuery : IRequest<IEnumerable<EmployeeDto>>
    {
        public Guid CompanyId { get; }
        public EmployeeParameters EmployeeParameters { get; }
        public bool TrackChanges { get; }

        public GetEmployeesQuery(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
        {
            CompanyId = companyId;
            EmployeeParameters = employeeParameters;
            TrackChanges = trackChanges;
        }
    }
}
