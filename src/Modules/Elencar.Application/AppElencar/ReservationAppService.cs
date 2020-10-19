using Elencar.Application.AppElencar.Input;
using Elencar.Application.AppElencar.Input.ObjectValue;
using Elencar.Application.AppElencar.Interfaces;
using Elencar.Domain.Entities;
using Elencar.Domain.Interfaces.Repositories;
using Marraia.Notifications.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Application.AppElencar
{
    public class ReservationAppService : IReservationAppService
    {
        private readonly ISmartNotification _notification;
        private readonly IActorRepository _actorRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IReservationRepository _reservationRepository;


        public ReservationAppService(ISmartNotification notification, IActorRepository actorRepository, IUserRepository userRepository
                                , IGenreRepository genreRepository, IReservationRepository reservationRepository)
        {
            _notification = notification;
            _actorRepository = actorRepository;
            _userRepository = userRepository;
            _genreRepository = genreRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<Reservation>> Get()
        {
            var reservation = await _reservationRepository.Get();

            if (reservation == default)
            {
                _notification.NewNotificationBadRequest("No result.");
                return default;
            }

            return reservation;
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {

            var reservation = await _reservationRepository
                            .GetByIdAsync(id)
                            .ConfigureAwait(false);

            if (reservation == default)
            {
                _notification.NewNotificationBadRequest("No result.");
                return default;
            }
            return reservation;
        }

        public async Task<Reservation> Insert(ReservationInput reservationInput)
        {
            var genre = await _genreRepository.GetByIdAsync(reservationInput.GenreId);
            var producer = await _userRepository.GetByIdAsync(reservationInput.ProducerId);
            var actor = await _actorRepository.GetByIdAsync(reservationInput.ActorId);
            var reservation = new Reservation(reservationInput.Name, DateTime.Parse(reservationInput.Start, new CultureInfo("pt-BR")), DateTime.Parse(reservationInput.End, new CultureInfo("pt-BR"))
                                                , genre,producer,actor);


            var newReservation = await _reservationRepository.Insert(reservation);

            if (newReservation == default)
            {
                _notification.NewNotificationBadRequest("Invalid reservation values!");
                return default;
            }


            return newReservation;

        }
        public async Task<Reservation> Update(ReservationInputUpdate reservationInputUpdate)
        {
            var genre = await _genreRepository.GetByIdAsync(reservationInputUpdate.GenreId);
            var producer = await _userRepository.GetByIdAsync(reservationInputUpdate.ProducerId);
            var actor = await _actorRepository.GetByIdAsync(reservationInputUpdate.ActorId);
            var reservation = new Reservation(reservationInputUpdate.ReservationId, reservationInputUpdate.Name, DateTime.Parse(reservationInputUpdate.Start, new CultureInfo("pt-BR")), DateTime.Parse(reservationInputUpdate.End, new CultureInfo("pt-BR"))
                                                , genre, producer, actor);

            var newReservation = await _reservationRepository.Update(reservation);

            if (newReservation == default)
            {
                _notification.NewNotificationBadRequest("Invalid Reservation values!");
                return default;
            }
            return newReservation;
        }

        public void Delete(int id)
        {
            _reservationRepository.Delete(id);
        }

       
    }
}
