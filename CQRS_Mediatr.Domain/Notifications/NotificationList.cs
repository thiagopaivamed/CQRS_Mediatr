using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_Mediatr.Domain.Notifications
{
    public class NotificationList
    {
        private readonly List<Notification> notifications;
        public IReadOnlyCollection<Notification> Notifications => this.notifications;
        public bool HasNotifications => notifications.Any();

        public NotificationList()
        {
            this.notifications = new List<Notification>();
        }

        public void AddNotification(string key, string message)
        {
            notifications.Add(new Notification(key, message));
        }

        public void AddNotifications (ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                this.AddNotification(error.ErrorCode, error.ErrorMessage);
            }
        }
    }
}
