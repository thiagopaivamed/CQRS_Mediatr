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
    public class GetAllCustomersQuery : IRequest<IEnumerable<Customer>>
    {
        public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<Customer>>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;

            public GetAllCustomersQueryHandler(ICustomerRepository customerRepository, IMediator mediator)
            {
                _customerRepository = customerRepository;
                _mediator = mediator;
            }

            public async Task<IEnumerable<Customer>> Handle(GetAllCustomersQuery query, CancellationToken cancellationToken)
            {
                var customerList = await _customerRepository.GetAll();

                if (customerList == null)
                {
                    await _mediator.Publish(new ErrorNotification
                    {
                        Error = "Customers not found",
                        Stack = "Customers are null",
                    }, cancellationToken);
                    return default; 
                }

                return customerList;
            }
        }
    }
}
