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
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }
        public async void Delete(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"DELETE FROM [dbo].[User] WHERE id = {id}";

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

        public IEnumerable<User> Get()
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var UserList = new List<User>();
                    var sqlCmd = $@"SELECT u.id, u.name, u.email, u.status, u.roleId, r.name as papel
                                        FROM [dbo].[User] u
                                INNER JOIN [dbo].[Role] r on u.roleId = r.id
                                   Where status = 'True'";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();
                        var reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                            var user = new User(int.Parse(reader["id"].ToString()),
                                                reader["name"].ToString(), reader["email"].ToString(), bool.Parse(reader["status"].ToString()),
                                                new Role(int.Parse(reader["roleId"].ToString()),
                                                                    reader["papel"].ToString())
                                                );
                            UserList.Add(user);
                            
                            }
                        return UserList;

                        }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = @$"SELECT id FROM [dbo].[User] 
                                                    WHERE email ='{email}'";
                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        var reader = await cmd
                                            .ExecuteReaderAsync()
                                            .ConfigureAwait(false);
                        while (reader.Read())
                        {
                            var user = new User(reader["id"].ToString(), reader["name"].ToString(), reader["email"].ToString(), new Role(int.Parse(reader["roleId"].ToString())));
                            return user;
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

        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = @$"SELECT u.id, u.name, u.email, u.roleId, r.name as papel FROM [dbo].[User] u
                                            INNER JOIN [dbo].[Role] r on u.roleId = r.id
                                                    WHERE u.id ='{id}'";
                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();
                        var reader = await cmd
                                            .ExecuteReaderAsync()
                                            .ConfigureAwait(false);
                        while (reader.Read())
                        {
                            var user = new User(int.Parse(reader["id"].ToString()),
                                                reader["name"].ToString(), reader["email"].ToString(), 
                                                new Role(int.Parse(reader["roleId"].ToString()), 
                                                                    reader["papel"].ToString())
                                                );
                            return user;
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

        public async Task<int> Insert(User user)
        {
            try
            {
                var hasAccount = GetByEmailAsync(user.Email);

                if (hasAccount == default)
                {
                 return default;
                }

                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {

                    var sqlCmd = @"INSERT INTO [dbo].[User] (
                                                name,
                                                email,
                                                password,
                                                roleId
                                                )
                                           VALUES (
                                                @name,
                                                @email,
                                                @password,
                                                1
                                            ); SELECT scope_identity();";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("name", user.Name);
                        cmd.Parameters.AddWithValue("email", user.Email);
                        cmd.Parameters.AddWithValue("password", user.Password);
                        cmd.Parameters.AddWithValue("roleId", user.Role.Id);
                        con.Open();
                        var id = await cmd.ExecuteScalarAsync().ConfigureAwait(false);
                        return int.Parse(id.ToString());
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Task<User> Update(User actor)
        {
            throw new NotImplementedException();
        }
    }
}
