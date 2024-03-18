using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Company
{
    public sealed class DeleteCompanyCommand : IRequest
    {
        public Guid Id { get; }
        public bool TrackChanges { get; }

        public DeleteCompanyCommand(Guid id, bool trackChanges)
        {
            Id = id;
            TrackChanges = trackChanges;
        }
    }
}
