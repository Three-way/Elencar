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

        public ActorRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }

        public async Task<IEnumerable<Actor>> Get()
        {
            try
            {
                var actorList = new List<Actor>();
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = @$"SELECT u.name, u.email, u.roleId, r.name as papel, a.fee, a.bio, a.userId FROM [dbo].[Actor] a
                                            INNER JOIN [dbo].[User] u on u.id = a.userId
                                            INNER JOIN [dbo].[Role] r on r.id = u.roleId";
                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        var reader = await cmd
                                            .ExecuteReaderAsync()
                                            .ConfigureAwait(false);

                        while (reader.Read())
                        {
                            var actor = new Actor(reader["id"].ToString(), (decimal)(reader["fee"]),
                                                new User(int.Parse(reader["userId"].ToString()), reader["name"].ToString(), reader["email"].ToString(),
                                                new Role(int.Parse(reader["roleId"].ToString()), reader["papel"].ToString()))
                                                );
                            actorList.Add(actor);
                        }
                        return actorList;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Actor> EnrolledActor(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = @$"SELECT u.name, u.email, u.roleId, r.name as papel, a.fee, a.bio, a.userId FROM [dbo].[Actor] a
                                            INNER JOIN [dbo].[User] u on u.id = a.userId
                                            INNER JOIN [dbo].[Role] r on r.id = u.roleId
                                                    WHERE a.userId ='{id}'";
                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        var reader = await cmd
                                            .ExecuteReaderAsync()
                                            .ConfigureAwait(false);

                        while (reader.Read())
                        {
                            var actor = new Actor(reader["id"].ToString(), (decimal)(reader["fee"]),
                                                new User(int.Parse(reader["userId"].ToString()), reader["name"].ToString(), reader["email"].ToString(),
                                                new Role(int.Parse(reader["roleId"].ToString()), reader["papel"].ToString()))
                                                );
                            return actor;
                        }
                        return default;
                    }
                }
            }
            catch (Exception ex)
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
                    var sqlCmd = @$"SELECT u.name, u.email, u.roleId, r.name as papel, a.fee, a.bio, a.userId FROM [dbo].[Actor] a
                                            INNER JOIN [dbo].[User] u on u.id = a.userId
                                            INNER JOIN [dbo].[Role] r on r.id = u.roleId
                                                    WHERE a.id ='{id}'";
                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();
                        var reader = await cmd
                                            .ExecuteReaderAsync()
                                            .ConfigureAwait(false);
                        while (reader.Read())
                        {
                            var actor = new Actor(id,reader["bio"].ToString(), (decimal)(reader["fee"]),
                                                new User(int.Parse(reader["userId"].ToString()), reader["name"].ToString(), reader["email"].ToString(),
                                                new Role(int.Parse(reader["roleId"].ToString()),reader["papel"].ToString()))
                                                );
                            return actor;
                        }
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Actor> Insert(Actor actor)
        {
            try
            {
                var hasAccount = EnrolledActor(actor.User.Id);

                if (hasAccount == default)
                {
                    return default;
                }

                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {

                    var sqlCmd = @"INSERT INTO [dbo].[Actor] (
                                                userId,
                                                fee,
                                                bio
                                                )
                                           VALUES (
                                                @userId,
                                                @fee,
                                                @bio
                                            ); SELECT scope_identity();";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("name", actor.User.Id);
                        cmd.Parameters.AddWithValue("email", actor.Fee);
                        cmd.Parameters.AddWithValue("password", actor.Bio);
                        con.Open();
                        var id = await cmd.ExecuteScalarAsync().ConfigureAwait(false);
                        var actorReturn = await GetByIdAsync(int.Parse(id.ToString()));
                        return actorReturn;
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Actor> Update(Actor actor)
        {
            return default;
        }

        public async void Delete(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"DELETE FROM [dbo].[Actor] WHERE id = {id}";

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

    }
}
