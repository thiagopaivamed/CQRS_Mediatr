using CQRS_Mediatr.Application.Services.Notifications;
using CQRS_Mediatr.Domain.Entities;
using CQRS_Mediatr.Domain.Interfaces;
using CQRS_Mediatr.Domain.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Mediatr.Application.Commands.Handlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediator _mediator;
        private readonly NotificationList _notificationList;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IMediator mediator, NotificationList notificationList)
        {
            _customerRepository = customerRepository;
            _mediator = mediator;
            _notificationList = notificationList;
        }

        public async Task<int> Handle(CreateCustomerCommand command, CancellationToken cancelationToken)
        {
            var customer = new Customer(command.Name, command.Email);

            if (customer.Invalid)
                _notificationList.AddNotifications(customer.ValidationResult);            

            else
            {
                _customerRepository.Add(customer);
                await _mediator.Publish(new CustomerActionNotification
                {
                    Name = customer.Name,
                    Email = customer.Email,
                    Action = ActionNotification.Created
                }, cancelationToken);
                                
            }
            return customer.Id;
        }
    }
}
