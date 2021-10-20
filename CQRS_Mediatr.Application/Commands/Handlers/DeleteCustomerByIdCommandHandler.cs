using CQRS_Mediatr.Application.Services.Notifications;
using CQRS_Mediatr.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Mediatr.Application.Commands.Handlers
{
    public class DeleteCustomerByIdCommandHandler : IRequestHandler<DeleteCustomerByIdCommand, int>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediator _mediator;

        public DeleteCustomerByIdCommandHandler(ICustomerRepository customerRepository, IMediator mediator)
        {
            _customerRepository = customerRepository;
            _mediator = mediator;
        }

        public async Task<int> Handle(DeleteCustomerByIdCommand command, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetById(command.CustomerId);

            if (customer == null)
            {
                await _mediator.Publish(new ErrorNotification
                {
                    Error = "Customer not found",
                    Stack = "Customer is null",
                }, cancellationToken);
                return default;
            }


            _customerRepository.Remove(customer);

            await _mediator.Publish(new CustomerActionNotification
            {
                Name = customer.Name,
                Email = customer.Email,
                Action = ActionNotification.Updated
            }, cancellationToken);

            return customer.Id;
        }
    }
}
