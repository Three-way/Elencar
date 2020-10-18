using Elencar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Application.AppElencar.Interfaces
{
    public interface IUserReservationAppService
    {
        Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId);
    }
}
