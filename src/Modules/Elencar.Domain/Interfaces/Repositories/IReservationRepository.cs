using Elencar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Domain.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> Get();
        Task<Reservation> GetByIdAsync(int id);
        Task<Reservation> Insert(Reservation reservation);
        Task<Reservation> Update(Reservation reservation);
        void Delete(Reservation reservation);
    }
}
