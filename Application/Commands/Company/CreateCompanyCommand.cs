using MediatR;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Company
{
    public sealed class CreateCompanyCommand : IRequest<CompanyDto>
    {
        public CompanyForCreationDto Company { get; }

        public CreateCompanyCommand(CompanyForCreationDto company)
        {
            Company = company;
        }
    }
}
