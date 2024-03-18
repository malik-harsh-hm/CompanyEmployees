using MediatR;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Company
{
    public sealed class GetCompanyQuery : IRequest<CompanyDto>
    {
        public Guid Id { get; }
        public bool TrackChanges { get; }

        public GetCompanyQuery(Guid id, bool trackChanges)
        {
            Id = id;
            TrackChanges = trackChanges;
        }
    }
}
