using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_Mediatr.Application.Services.Notifications
{
    public enum ActionNotification
    {
        Selected = 1,
        Created = 2,
        Updated = 3,
        Deleted = 4        
    }
}
