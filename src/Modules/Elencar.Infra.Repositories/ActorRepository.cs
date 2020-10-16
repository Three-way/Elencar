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
    public class ActorRepository : IActorRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IProfileRepository _profileRepository;

        public ActorRepository(IConfiguration configuration, IProfileRepository profileRepository)
        {
            _configuration = configuration;
            _profileRepository = profileRepository;
        }

        public async Task<IEnumerable<Actor>> Get(int? quantity = 10, int? idGenre = null, DateTime? startDate = null
                                                        , decimal? budget = null, int? orderbyVal = 0, int orderByRel = 1)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var actorList = new List<Actor>();

                    var filtterQuantity = "";
                    if (quantity != null)
                        filtterQuantity = $" TOP {quantity}";

                    var filterGenre = "";
                    if (idGenre != null)
                        filterGenre = $" AND g.genderId = {idGenre}";

                    var filterStartDate = "";
                    if (startDate != null)
                        filterStartDate = $" AND {startDate}  not between r.startAt and r.endAt";

                    var filterBudget = "";
                    if (budget != null)
                        filterBudget = $" AND  p.cache <= {budget}";


                    var orderVal = "";
                    if (orderbyVal != 1)
                        orderVal = " p.fee ";

                    var orderRel = "";
                    if (orderByRel != 1)
                        orderRel = " count(ur.profileId) desc";



                    var sqlCmd = @"SELECT {0} u.id as userId, u.name , p.id as profileId
                                                                , p.fee, g.id as genreId, g.description 
                                        FROM User u
                                        JOIN Profile p on p.user_id = u.id
                                        JOIN Genre g on g.id = p.genre_Id
                                        JOIN User_Reservation ur on ur.profile_Id = p.id
                                        JOIN Reservation r on r.id = ur.reservation_Id
                                        WHERE p.status = 1 AND u.status = 1 and u.status = 1
                                        {1}{2}{3}
                                        ORDER BY
                                        {4}{5}";

                    sqlCmd = string.Format(sqlCmd,
                                            filtterQuantity,
                                            filterGenre,
                                            filterStartDate,
                                            orderRel,
                                            orderVal);

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        var reader = await cmd.ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                        while (reader.Read())
                        {
                            var user = new Actor()
                            {
                                Id = Int32.Parse(reader["userId"].ToString()),
                                Name = reader["name"].ToString(),
                                Profile = new Profile()
                                {
                                    Id = Int32.Parse(reader["profileId"].ToString()),
                                    Fee = Decimal.Parse(reader["fee"].ToString()),
                                    Genre = new Genre()
                                    {
                                        Id = Int32.Parse(reader["genreId"].ToString()),
                                        Description = reader["description"].ToString()
                                    }
                                }
                            };

                            actorList.Add(user);
                        }

                        return actorList;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Actor> GetByIdAsync(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"SELECT u.id as userId, u.name , p.id as profileId, p.fee, p.bio
                                           , g.id as genreId, g.description
                                           FROM [dbo].[User] u
                                           JOIN [dbo].[Profile] p on p.user_id = u.id
                                           JOIN [dbo].[Genre] g on g.id = p.genre_Id
                                           WHERE p.status = 1 AND u.status = 1 AND u.Id = {id}";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                        while (reader.Read())
                        {

                            var actor = new Actor()
                            {
                                Id = Int32.Parse(reader["userId"].ToString()),
                                Name = reader["Name"].ToString(),
                                Profile = new Profile()
                                {
                                    Id = Int32.Parse(reader["profileId"].ToString()),
                                    Bio = reader["Bio"].ToString(),
                                    Genre = new Genre()
                                    {
                                        Id = Int32.Parse(reader["genreId"].ToString()),
                                        Description = reader["description"].ToString()
                                    }
                                }
                            };

                            return actor;
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

        public async Task<Actor> Insert(Actor actor)
        {
            try
            {
                //var hasAccount = await GetByEmailAsync(actor.Email);
                //if (hasAccount)
                //{
                   // return null;
                //}

                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {

                    var sqlCmd = @"INSERT INTO [dbo].[User] (
                                                Name,
                                                Email,
                                                Password,
                                                isProducer,
                                                Status
                                                )
                                           VALUES (
                                                @name,
                                                @email,
                                                @password,
                                                0,
                                                1
                                            ); SELECT scope_Identity();";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("name", actor.Name);
                        cmd.Parameters.AddWithValue("email", actor.Email);
                        cmd.Parameters.AddWithValue("password", actor.Password);
                        //cmd.Parameters.AddWithValue("isProducer", actor.IsProducer);

                        con.Open();
                        var id = await cmd.ExecuteScalarAsync().ConfigureAwait(false);

                        var profile = new Profile() { Bio = actor.Profile.Bio, Fee = actor.Profile.Fee
                                                            , Actor = new Actor() { Id = int.Parse(id.ToString()) }
                                                            , Genre = new Genre() { Id = actor.Profile.Genre.Id }    };

                        var newProfile = await _profileRepository.Insert(profile);

                        actor.Id = int.Parse(id.ToString());
                        actor.Profile.Bio = profile.Bio;
                        actor.Profile.Fee = profile.Fee;
                        actor.Status = true;
                        actor.CreatedAt = DateTime.Now;
                        actor.UpdatedAt = DateTime.Now;
                        //return await GetByIdAsync(int.Parse(id.ToString()));
                        return actor;
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return default;
            }
        }

        public async Task<Actor> Update(Actor actor)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"UPDATE [dbo].[User] set
                                                Name = @name,
                                                Email = @email,
                                                Password = @password,
                                                isProducer = @isProducer
                                    WHERE id = {actor.Id}";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("name", actor.Name);
                        cmd.Parameters.AddWithValue("email", actor.Email);
                        cmd.Parameters.AddWithValue("password", actor.Password);
                        cmd.Parameters.AddWithValue("isProducer", actor.IsProducer);

                        con.Open();
                        return await GetByIdAsync(actor.Id);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default;
            }

        }

        public async void Delete(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"DELETE FROM User WHERE ID = {id}";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> HasActor(string email)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"SELECT Email FROM [dbo].[User] WHERE Email = '{email}'";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                        if (!reader.Read())
                        {
                            return false;
                        }

                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default;
            }
        }
    }
}
