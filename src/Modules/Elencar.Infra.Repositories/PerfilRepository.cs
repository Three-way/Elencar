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
    public class PerfilReopository : IPerfilRepository
    {
        private readonly IConfiguration _configuration;

        public PerfilReopository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Perfil> GetByIdAsync(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"SELECT *  
                                           FROM Perfil
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
                            var perfil = new Perfil(Int32.Parse(reader["Id"].ToString()),reader["Description"].ToString());
                            

                            return perfil;
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
    }
}
