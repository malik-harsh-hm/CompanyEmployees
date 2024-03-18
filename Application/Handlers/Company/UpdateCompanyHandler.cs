using Application.Commands.Company;
using AutoMapper;
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
    internal sealed class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public UpdateCompanyHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var companyEntity = await _repository.Company.GetCompany(request.Id, request.TrackChanges);

            if (companyEntity is null)
                throw new CompanyNotFoundException(request.Id);

            _mapper.Map(request.Company, companyEntity);

            await _repository.SaveAsync();
        }
    }
}
