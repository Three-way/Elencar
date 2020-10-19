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
    public class RoleRepository : IRoleRepository
    {
        private readonly IConfiguration _configuration;

        public RoleRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async void Delete(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"DELETE FROM Role WHERE ID = {id}";

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

        public async Task<IEnumerable<Role>> Get()
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var roleList = new List<Role>();
                    var sqlCmd = $@"SELECT * FROM [dbo].[Role]";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                        while (reader.Read())
                        {

                            var role = new Role(Int32.Parse(reader["id"].ToString()), reader["name"].ToString(), (DateTime)reader["createdAt"]);



                            roleList.Add(role);
                        }
                        return roleList;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"SELECT * FROM [dbo].[Role]                                           
                                           WHERE Id = {id}";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                        while (reader.Read())
                        {

                            var role = new Role(id, reader["name"].ToString());

                            return role;
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

        public async Task<Role> Insert(Role role)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var query = @"INSERT INTO [dbo].[Role] (name) VALUES (@name);
                                    SELECT scope_identity();";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("name", role.Name);
                        con.Open();
                        var id = await cmd.ExecuteScalarAsync().ConfigureAwait(false);

                        return await GetByIdAsync(role.Id);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Role> Update(Role role)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"UPDATE Role set
                                                Name = @name
                                    WHERE id = {role.Id}";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("name", role.Name);

                        con.Open();
                        return await GetByIdAsync(role.Id);
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
