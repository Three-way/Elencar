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
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(User user)
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
                        var id = cmd.ExecuteScalar();

   
                        return (int)id;
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
