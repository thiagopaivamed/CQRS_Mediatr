using CQRS_Mediatr.Application.Services.Notifications;
using CQRS_Mediatr.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS_Mediatr.Application.Commands
{
    public class DeleteCustomerByIdCommand : IRequest<int>
    {
        public int CustomerId { get; set; }
        
    }
}
