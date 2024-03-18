using Application.Queries.Company;
using AutoMapper;
using Contracts;
using MediatR;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Company
{
    internal sealed class GetCompaniesHandler : IRequestHandler<GetCompaniesQuery, IEnumerable<CompanyDto>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public GetCompaniesHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyDto>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            // Implement your logic to retrieve companies from the repository
            var companies = await _repository.Company.GetAllCompanies(request.TrackChanges); 
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies); 
            return companiesDto;
        }
    }
}
