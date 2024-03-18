using MediatR;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Employee
{
    public sealed class UpdateEmployeeForCompanyCommand : IRequest
    {
        public Guid CompanyId { get; }
        public Guid EmployeeId { get; }
        public EmployeeForUpdationDto EmployeeForUpdation { get; }
        public bool CompTrackChanges { get; }
        public bool EmpTrackChanges { get; }

        public UpdateEmployeeForCompanyCommand(Guid companyId, Guid employeeId, EmployeeForUpdationDto employeeForUpdation, bool compTrackChanges, bool empTrackChanges)
        {
            CompanyId = companyId;
            EmployeeId = employeeId;
            EmployeeForUpdation = employeeForUpdation;
            CompTrackChanges = compTrackChanges;
            EmpTrackChanges = empTrackChanges;
        }
    }
}
