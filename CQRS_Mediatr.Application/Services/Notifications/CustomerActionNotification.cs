using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_Mediatr.Application.Services.Notifications
{
    public class CustomerActionNotification : INotification
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public ActionNotification Action { get; set; }
    }
}
