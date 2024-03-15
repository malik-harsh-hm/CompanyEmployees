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
        public IActionResult GetCompanies()
        {
            var companies = _service.CompanyService.GetAllCompanies(trackChanges: false);
            return Ok(companies);

            // no try - catch as its handled globally
        }
        [HttpGet]
        [Route("{companyId:guid}", Name = "GetCompany")]
        public IActionResult GetCompany(Guid companyId)
        {
            var company = _service.CompanyService.GetCompany(companyId, trackChanges: false);
            return Ok(company);

            // no try - catch as its handled globally
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company is null)
                return BadRequest("CompanyForCreationDto object is null");

            var createdCompany = _service.CompanyService.CreateCompany(company);

            return CreatedAtRoute("GetCompany", new
            {
                companyId = createdCompany.Id
            }, createdCompany);
        }

        [HttpDelete("{companyId:guid}")]
        public IActionResult DeleteCompany(Guid companyId)
        {
            _service.CompanyService.DeleteCompany(companyId, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{companyId:guid}")]
        public IActionResult UpdateCompany(Guid companyId, [FromBody] CompanyForUpdationDto company)
        {
            if (company is null) 
                return BadRequest("CompanyForUpdateDto object is null");

            _service.CompanyService.UpdateCompany(companyId, company, trackChanges: true);
            return NoContent();
        }
    }
}
