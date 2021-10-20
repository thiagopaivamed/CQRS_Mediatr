using CQRS_Mediatr.Application.Services.Notifications;
using CQRS_Mediatr.Domain.Entities;
using CQRS_Mediatr.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Mediatr.Application.Queries
{
    public class GetCustomerByIdQuery : IRequest<Customer>
    {
        public int CustomerId { get; set; }

        public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;

            public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMediator mediator)
            {
                _customerRepository = customerRepository;
                _mediator = mediator;
            }

            public async Task<Customer> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
            {
                var customer = await _customerRepository.GetById(query.CustomerId);

                if (customer == null)
                {
                    await _mediator.Publish(new ErrorNotification
                    {
                        Error = "Customer not found",
                        Stack = "Customer is null",
                    }, cancellationToken);
                    return default;
                }

                await _mediator.Publish(new CustomerActionNotification
                {
                    Name = customer.Name,
                    Email = customer.Email,
                    Action = ActionNotification.Selected
                }, cancellationToken);

                return customer;
            }
        }

    }
}
