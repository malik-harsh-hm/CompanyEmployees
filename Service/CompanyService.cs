using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.ComponentModel.Design;

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

        public async Task<CompanyDto> CreateCompany(CompanyForCreationDto company)
        {
            var companyEntity = _mapper.Map<Company>(company);

            _repository.Company.CreateCompany(companyEntity);
            await _repository.SaveAsync();

            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);

            return companyToReturn;
        }
        public async Task DeleteCompany(Guid companyId, bool trackChanges)
        {
            var companyEntity = await _repository.Company.GetCompany(companyId, trackChanges);
            if (companyEntity is null)
                throw new CompanyNotFoundException(companyId);

            _repository.Company.DeleteCompany(companyEntity); 
            await _repository.SaveAsync();
        }
        public async Task<IEnumerable<CompanyDto>> GetAllCompanies(bool trackChanges)
        {
            var companies = await _repository.Company.GetAllCompanies(trackChanges);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return companiesDto;

            // no try - catch as its handled globally
        }
        public async Task<CompanyDto> GetCompany(Guid companyId, bool trackChanges)
        {
            var companyEntity = await _repository.Company.GetCompany(companyId, trackChanges);
            if (companyEntity is null)
                throw new CompanyNotFoundException(companyId);

            var companyDto = _mapper.Map<CompanyDto>(companyEntity);
            return companyDto;
        }
        public async Task UpdateCompany(Guid companyId, CompanyForUpdationDto companyForUpdate, bool trackChanges)
        {
            var companyEntity = await _repository.Company.GetCompany(companyId, trackChanges);
            if (companyEntity is null)
                throw new CompanyNotFoundException(companyId);

            // Connected Update:
            // We are mapping from the companyForUpdate object
            // to the companyEntity — thus changing the state of the companyEntity object to Modified.
            _mapper.Map(companyForUpdate, companyEntity); 
            await _repository.SaveAsync();

        }
    }
}
