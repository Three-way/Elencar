using Elencar.Application.AppElencar.Input;
using Elencar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Application.AppElencar.Interfaces
{
    public interface IReservationAppService
    {
        Task<IEnumerable<Reservation>> Get();
        Task<Reservation> GetByIdAsync(int id);
        Task<Reservation> Insert(ReservationInput reservationInput);
        Task<Reservation> Update(ReservationInput reservationInput);
        void Delete(int id);
    }
}
