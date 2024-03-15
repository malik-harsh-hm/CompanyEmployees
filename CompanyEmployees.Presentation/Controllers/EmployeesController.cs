﻿using Entities.Exceptions;
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
    [Route("api/companies/{companyId}/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public EmployeesController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetEmployees(Guid companyId)
        {
            var employees = _service.EmployeeService.GetEmployees(companyId, trackChanges: false);
            return Ok(employees);

            // no try - catch as its handled globally
        }

        [HttpGet]
        [Route("{employeeId:guid}", Name = "GetEmployee")]
        public IActionResult GetEmployee(Guid companyId, Guid employeeId)
        {
            var employee = _service.EmployeeService.GetEmployee(companyId, employeeId, trackChanges: false);
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee)
        {
            if (employee is null)
                return BadRequest("EmployeeForCreationDto object is null");

            if (!ModelState.IsValid) 
                return UnprocessableEntity(ModelState);

            var employeeToReturn = _service.EmployeeService.CreateEmployeeForCompany(companyId, employee, trackChanges: false);

            return CreatedAtRoute("GetEmployee", new
            {
                companyId,
                employeeId = employeeToReturn.Id
            }, employeeToReturn);
        }

        [HttpDelete("{employeeId:guid}")]
        public IActionResult DeleteEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            _service.EmployeeService.DeleteEmployeeForCompany(companyId, employeeId, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{employeeId:guid}")]
        public IActionResult UpdateEmployeeForCompany(Guid companyId, Guid employeeId, [FromBody] EmployeeForUpdationDto employee)
        {
            if (employee is null) 
                return BadRequest("EmployeeForUpdateDto object is null");

            if (!ModelState.IsValid) 
                return UnprocessableEntity(ModelState);

            _service.EmployeeService.UpdateEmployeeForCompany(companyId, employeeId, employee, compTrackChanges: false, empTrackChanges: true); // Track Employee changes - as soon as we change any property in this entity, EF Core will set the state of that entity to Modified.
            return NoContent();
        }
    }
}
