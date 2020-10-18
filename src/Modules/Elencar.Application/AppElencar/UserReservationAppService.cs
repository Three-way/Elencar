using Elencar.Application.AppElencar.Interfaces;
using Elencar.Domain.Entities;
using Elencar.Domain.Interfaces.Repositories;
using Marraia.Notifications.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Application.AppElencar
{
    public class UserReservationAppService : IUserReservationAppService
    {
        private readonly IUserReservationRepository _userReservationRepository;
        private readonly ISmartNotification _notification;



        public UserReservationAppService(ISmartNotification notification, IUserReservationRepository userReservationRepository)
        {
            _notification = notification;
            _userReservationRepository = userReservationRepository;
        }
        
        public async Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId)
        {
            var userReservations = await _userReservationRepository.GetReservationsByUserIdAsync(userId);

            if (userReservations == default)
            {
                _notification.NewNotificationBadRequest("No result.");
                return default;
            }
            return userReservations;
        }
    }
}
