using CompanyEmployees.Filters.ActionFilters;
using Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.Controllers
{
    [ApiController]
    [Route("api/companies/{companyId}/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public EmployeesController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees(Guid companyId, [FromQuery] EmployeeParameters employeeParameters)
        {
            var employees = await _service.EmployeeService.GetEmployees(companyId, employeeParameters, trackChanges: false);
            return Ok(employees);

            // no try - catch as its handled globally
        }

        [HttpGet]
        [Route("{employeeId:guid}", Name = "GetEmployee")]
        public async Task<IActionResult> GetEmployee(Guid companyId, Guid employeeId)
        {
            var employee = await _service.EmployeeService.GetEmployee(companyId, employeeId, trackChanges: false);
            return Ok(employee);
        }

        [HttpPost]
        [ValidationActionFilter]
        public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee)
        {
            if (employee is null)
                return BadRequest("EmployeeForCreationDto object is null");

            if (!ModelState.IsValid) 
                return UnprocessableEntity(ModelState);

            var employeeToReturn = await _service.EmployeeService.CreateEmployeeForCompany(companyId, employee, trackChanges: false);

            return CreatedAtRoute("GetEmployee", new
            {
                companyId,
                employeeId = employeeToReturn.Id
            }, employeeToReturn);
        }

        [HttpDelete("{employeeId:guid}")]
        public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            await _service.EmployeeService.DeleteEmployeeForCompany(companyId, employeeId, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{employeeId:guid}")]
        [ValidationActionFilter]
        public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid employeeId, [FromBody] EmployeeForUpdationDto employee)
        {
            if (employee is null) 
                return BadRequest("EmployeeForUpdateDto object is null");

            if (!ModelState.IsValid) 
                return UnprocessableEntity(ModelState);

            await _service.EmployeeService.UpdateEmployeeForCompany(companyId, employeeId, employee, compTrackChanges: false, empTrackChanges: true); // Track Employee changes - as soon as we change any property in this entity, EF Core will set the state of that entity to Modified.
            return NoContent();
        }
    }
}
