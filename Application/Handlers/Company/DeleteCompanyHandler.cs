using Application.Commands.Company;
using Contracts;
using Entities.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Company
{
    internal sealed class DeleteCompanyHandler : IRequestHandler<DeleteCompanyCommand>
    {
        private readonly IRepositoryManager _repository;

        public DeleteCompanyHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _repository.Company.GetCompany(request.Id, request.TrackChanges);

            if (company is null)
                throw new CompanyNotFoundException(request.Id);

            _repository.Company.DeleteCompany(company);
            await _repository.SaveAsync();
        }
    }
}
