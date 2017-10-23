using AutoMapper;
using GigHubOk.DTOs;
using GigHubOk.Models;
using GigHubOk.Persistence;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GigHubOk.Controllers.Api
{
    public class NotificationsController : ApiController
    {

        private IUnitOfWork _unitofwork;

        public NotificationsController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }


        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            List<Notification> notification = _unitofwork.Notifications.NotificationsToUsers(userId);

            return notification.Select(Mapper.Map<Notification, NotificationDto>);
        }


        [HttpPost]
        public IHttpActionResult NotificationsIsRead()
        {
            var userId = User.Identity.GetUserId();
            List<UserNotification> notification = _unitofwork.Notifications.NotificationsIsRead(userId);

            notification.ForEach(n => n.Read());

            _unitofwork.Complete();

            return Ok();


        }

       
    }
}
