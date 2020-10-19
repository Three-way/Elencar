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
    public class ReservationRepository : IReservationRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IUserReservationRepository _userReservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IActorRepository _actorRepository;
        public ReservationRepository(IConfiguration configuration, IUserReservationRepository userReservationRepository, IUserRepository userRepository,
                                            IGenreRepository genreRepository, IActorRepository actorRepository)
        {
            _configuration = configuration;
            _userReservationRepository = userReservationRepository;
            _userRepository = userRepository;
            _genreRepository = genreRepository;
            _actorRepository = actorRepository;
        }
        public async void Delete(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {

                    _userReservationRepository.Delete(id);
                    var sqlCmd = @"DELETE FROM Reservation 
                                              WHERE
                                                Id = @reservationId ;";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("reservationId", id);
                        con.Open();

                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Reservation>> Get()
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var reservationList = new List<Reservation>();
                    var sqlCmd = @"SELECT r.*, g.id as genreId, u.id as userId, a.id as actorId
                                                FROM Reservation r
                                                JOIN Genre g on g.id = r.genreId
                                                JOIN UserReservation ur on ur.reservationId = r.Id
                                                JOIN [dbo].[User] u on u.id = ur.producerId
                                                JOIN Actor a on a.id = ur.actorId
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
                            ;


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

        

        public async Task<Reservation> GetByIdAsync(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"SELECT r.*, g.id as genreId, u.id as userId, a.id as actorId
                                                FROM Reservation r
                                                JOIN Genre g on g.id = r.genreId
                                                JOIN UserReservation ur on ur.reservationId = r.Id
                                                JOIN [dbo].[User] u on u.id = ur.producerId
                                                JOIN Actor a on a.id = ur.actorId
                                 WHERE r.Id = {id}
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

                            return reservation;
                        }

                        return default;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Reservation> Insert(Reservation reservation)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = @"INSERT INTO Reservation (
                                                name,
                                                startAt,
                                                endAt,
                                                genreId
                                                )
                                           VALUES (
                                                @name,
                                                @startAt,
                                                @endAt,
                                                @genreId
                                            ); SELECT scope_identity();";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("name", reservation.Name);
                        cmd.Parameters.AddWithValue("startAt", reservation.Start);
                        cmd.Parameters.AddWithValue("endAt", reservation.End);
                        cmd.Parameters.AddWithValue("genreId", reservation.Genre.Id);
                        con.Open();

                        var id = await cmd.ExecuteScalarAsync().ConfigureAwait(false);

                        var userReservation = new UserReservation(Int32.Parse(id.ToString()), reservation.Producer.Id, reservation.Actor.Id);
                        _userReservationRepository.Insert(userReservation);

                        return await GetByIdAsync(Int32.Parse(id.ToString()));

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }




        }

        public async Task<Reservation> Update(Reservation reservation)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = @"UPDATE Reservation SET
                                                name = @name,
                                                startAt = @startAt,
                                                endAt = @endAt,
                                                genreId = @genreId
                                              WHERE
                                                Id = @reservationId
                                                ; SELECT scope_identity();";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("name", reservation.Name);
                        cmd.Parameters.AddWithValue("startAt", reservation.Start);
                        cmd.Parameters.AddWithValue("endAt", reservation.End);
                        cmd.Parameters.AddWithValue("genreId", reservation.Genre.Id);
                        cmd.Parameters.AddWithValue("reservationId", reservation.Id);
                        con.Open();

                        await cmd.ExecuteScalarAsync().ConfigureAwait(false);

                        var userReservation = new UserReservation(reservation.Id, reservation.Producer.Id, reservation.Actor.Id);
                        _userReservationRepository.Update(userReservation);

                        return await GetByIdAsync(reservation.Id);

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
