using CQRS_Mediatr.Application.Commands;
using CQRS_Mediatr.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS_Mediatr.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _mediator.Send(new GetAllCustomersQuery());
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand command)
        {
            int customerId = await _mediator.Send(command);
            return Ok(customerId);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetById(int customerId)
        {
            var customer = await _mediator.Send(new GetCustomerByIdQuery { CustomerId = customerId });

            if (customer == null)
                return null;

            return Ok(customer);
        }       
                
        [HttpPut("{id}")]        
        public async Task<IActionResult> Update(int id, UpdateCustomerCommand command)
        {
            if (id != command.CustomerId)
                return BadRequest();

            int customerId = await _mediator.Send(command);

            return Ok(customerId);
        }
                
                
        [HttpDelete("{id}")]       
        public async Task<IActionResult> Delete(int id)
        {
            var customerId = await _mediator.Send(new DeleteCustomerByIdCommand { CustomerId = id });

            return Ok(customerId);
        }
    }
}
