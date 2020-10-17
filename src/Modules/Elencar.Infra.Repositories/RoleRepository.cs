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
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Role> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Role role)
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
                        var id = cmd.ExecuteScalar();

                        return int.Parse(id.ToString());
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Role> Update(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
