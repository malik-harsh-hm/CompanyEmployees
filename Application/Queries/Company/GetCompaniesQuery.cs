using MediatR;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Company
{
    public sealed class GetCompaniesQuery : IRequest<IEnumerable<CompanyDto>>
    {
        public bool TrackChanges { get; }

        public GetCompaniesQuery(bool trackChanges)
        {
            TrackChanges = trackChanges;
        }
    }
}
