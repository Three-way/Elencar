﻿using Elencar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Domain.Interfaces.Repositories
{
    public interface IUserReservationRepository
    {
        Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId);
        void Insert(UserReservation reservation);
        void Update(UserReservation reservation);
        void Delete(int id);
    }
}
