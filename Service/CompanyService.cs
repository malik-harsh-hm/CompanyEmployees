using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public CompanyDto CreateCompany(CompanyForCreationDto company)
        {
            var companyEntity = _mapper.Map<Company>(company);

            _repository.Company.CreateCompany(companyEntity);
            _repository.Save();

            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);

            return companyToReturn;
        }

        public void DeleteCompany(Guid companyId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            _repository.Company.DeleteCompany(company); 
            _repository.Save();
        }

        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {
            var companies = _repository.Company.GetAllCompanies(trackChanges);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return companiesDto;

            // no try - catch as its handled globally
        }
        public CompanyDto GetCompany(Guid companyId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var companyDto = _mapper.Map<CompanyDto>(company);
            return companyDto;
        }
    }
}
