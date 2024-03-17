using CompanyEmployees.Filters.ActionFilters;
using Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public CompaniesController(IServiceManager service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _service.CompanyService.GetAllCompanies(trackChanges: false);
            return Ok(companies);

            // no try - catch as its handled globally
        }
        [HttpGet]
        [Route("{companyId:guid}", Name = "GetCompany")]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetCompany(Guid companyId)
        {
            var company = await _service.CompanyService.GetCompany(companyId, trackChanges: false);
            return Ok(company);

            // no try - catch as its handled globally
        }

        [HttpPost]
        [ValidationActionFilter]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company is null)
                return BadRequest("CompanyForCreationDto object is null");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var createdCompany = await _service.CompanyService.CreateCompany(company);

            return CreatedAtRoute("GetCompany", new
            {
                companyId = createdCompany.Id
            }, createdCompany);
        }

        [HttpDelete("{companyId:guid}")]
        public async Task<IActionResult> DeleteCompany(Guid companyId)
        {
            await _service.CompanyService.DeleteCompany(companyId, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{companyId:guid}")]
        [ValidationActionFilter]
        public async Task<IActionResult> UpdateCompany(Guid companyId, [FromBody] CompanyForUpdationDto company)
        {
            if (company is null)
                return BadRequest("CompanyForUpdateDto object is null");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _service.CompanyService.UpdateCompany(companyId, company, trackChanges: true);
            return NoContent();
        }
    }
}
