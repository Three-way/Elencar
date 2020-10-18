using Elencar.Domain.Entities;
using Elencar.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Infra.Repositories
{
    public class UserReservationRepository : IUserReservationRepository
    {
        private readonly IConfiguration _configuration;
        public UserReservationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async void Delete(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"DELETE FROM UserReservation 
                                                WHERE
                                                reservationId = {id};";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        await cmd.ExecuteScalarAsync().ConfigureAwait(false);

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async void Insert(UserReservation userReservation)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = @"INSERT INTO UserReservation (
                                                reservationId,
                                                producerId,
                                                actorId
                                                )
                                           VALUES (
                                                @reservationId,
                                                @producerId,
                                                @actorId
                                            );";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("reservationId", userReservation.ReservationId);
                        cmd.Parameters.AddWithValue("producerId", userReservation.ProducerId);
                        cmd.Parameters.AddWithValue("actorId", userReservation.ActorId);

                        await cmd.ExecuteScalarAsync().ConfigureAwait(false);

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async void Update(UserReservation userReservation)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = @"UPDATE UserReservation SET
                                                producerId = @producerId,
                                                actorId = @actorId
                                                WHERE
                                                reservationId = @reservationId
                                                ;";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("producerId", userReservation.ProducerId);
                        cmd.Parameters.AddWithValue("actorId", userReservation.ActorId);
                        cmd.Parameters.AddWithValue("reservationId", userReservation.ReservationId);

                        await cmd.ExecuteScalarAsync().ConfigureAwait(false);

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
