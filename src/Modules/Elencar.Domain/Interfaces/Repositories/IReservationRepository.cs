using Elencar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Domain.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        int Insert(Reservation reservation);
        Task<Reservation> GetByIdAsync(int id);
        IEnumerable<Reservation> Get();
        void Delete(Reservation reservation);
        int Update(Reservation reservation);
    }
}
