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
        //private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IActorRepository _actorRepository;
        public UserReservationRepository(IConfiguration configuration, IUserRepository userRepository,
                                            IGenreRepository genreRepository, IActorRepository actorRepository)
        {
            _configuration = configuration;
            //_reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _genreRepository = genreRepository;
            _actorRepository = actorRepository;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var reservationList = new List<Reservation>();
                    var sqlCmd = $@"SELECT r.*, g.id as genreId, u.id as userId, a.Id as actorId
                                                FROM [dbo].[Reservation] r
                                                JOIN [dbo].[Genre] g on g.id = r.genreId
                                                JOIN [dbo].[UserReservation] ur on ur.reservationId = r.Id
                                                JOIN [dbo].[Actor] a on a.id = ur.actorId
                                                JOIN [dbo].[User] u on u.id = ur.producerId
                                    WHERE u.Id = {userId} or a.userId = {userId}
";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        con.Open();
                        var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                        while (reader.Read())
                        {
                            
                            var genre = await _genreRepository.GetByIdAsync(Int32.Parse(reader["genreId"].ToString()));
                            var producer = await _userRepository.GetByIdAsync(Int32.Parse(reader["userId"].ToString()));
                            var actor = await _actorRepository.GetByIdAsync(Int32.Parse(reader["actorId"].ToString()));


                            var reservation = new Reservation(Int32.Parse(reader["Id"].ToString()),
                                                                reader["name"].ToString(),
                                                                DateTime.Parse(reader["startAt"].ToString()),
                                                                DateTime.Parse(reader["endAt"].ToString()),
                                                                genre,
                                                                producer,
                                                                actor);

                            reservationList.Add(reservation);
                        }

                        return reservationList;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async void Delete(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"DELETE FROM [dbo].[UserReservation] 
                                                WHERE
                                                reservationId = {id};";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();
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
                        con.Open();

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
                        con.Open();

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
