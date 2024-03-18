using MediatR;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Company
{
    public sealed class UpdateCompanyCommand : IRequest
    {
        public Guid Id { get; }
        public CompanyForUpdationDto Company { get; }
        public bool TrackChanges { get; }

        public UpdateCompanyCommand(Guid id, CompanyForUpdationDto company, bool trackChanges)
        {
            Id = id;
            Company = company;
            TrackChanges = trackChanges;
        }
    }
}
