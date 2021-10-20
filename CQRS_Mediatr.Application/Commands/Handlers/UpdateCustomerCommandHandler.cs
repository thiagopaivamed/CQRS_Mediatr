using CQRS_Mediatr.Application.Services.Notifications;
using CQRS_Mediatr.Domain.Entities;
using CQRS_Mediatr.Domain.Interfaces;
using CQRS_Mediatr.Domain.Notifications;
using CQRS_Mediatr.Domain.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Mediatr.Application.Commands.Handlers
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, int>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediator _mediator;
        private readonly NotificationList _notificationList;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMediator mediator, NotificationList notificationList)
        {
            _customerRepository = customerRepository;
            _mediator = mediator;
            _notificationList = notificationList;
        }

        public async Task<int> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
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

            customer.Name = command.Name;
            customer.Email = command.Email;

            customer.Validate(customer, new CustomerValidation());
            

            if (customer.Invalid)
                _notificationList.AddNotifications(customer.ValidationResult);

            else
            {
                _customerRepository.Update(customer);

                await _mediator.Publish(new CustomerActionNotification
                {
                    Name = command.Name,
                    Email = command.Email,
                    Action = ActionNotification.Updated
                }, cancellationToken);
            }

            return customer.Id;
        }
    }
}
