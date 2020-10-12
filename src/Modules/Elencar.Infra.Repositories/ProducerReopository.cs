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
    public class ProducerReopository : IProducerRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IPerfilRepository _perfilRepository;

        public ProducerReopository(IConfiguration configuration, IPerfilRepository perfilRepository)
        {
            _configuration = configuration;
            _perfilRepository = perfilRepository;
        }

        public IEnumerable<Producer> Get()
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var producerList = new List<Producer>();
                    var sqlCmd = @"SELECT Pd.Id,Pd.Name, Pd.Email, Pd.Password, Pf.Id as IdPerfil, Pf.Description  
                                           FROM Producer Pd
                                           JOIN Perfil Pf ON Pd.IdPerfil = Pf.Id";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var perfil = _perfilRepository.GetByIdAsync(Int32.Parse(reader["IdPerfil"].ToString()));
                            var producer = new Producer(Int32.Parse(reader["Id"].ToString()),
                                                        reader["Name"].ToString(),
                                                        reader["Email"].ToString(),
                                                        reader["Password"].ToString(),
                                                        perfil.Result);
                            
                            producerList.Add(producer);
                        }

                        return producerList;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message); 
            }
        }

        public async Task<Producer> GetByIdAsync(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"SELECT Pd.Name, Pd.Email, Pd.Password, Pf.Id as IdPerfil, Pf.Description  
                                           FROM Producer Pd
                                           JOIN Perfil Pf ON Pd.IdPerfil = Pf.Id
                                            WHERE Pd.Id = {id}";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                        while (reader.Read())
                        {
                            var perfil = _perfilRepository.GetByIdAsync(Int32.Parse(reader["IdPerfil"].ToString()));
                            var producer = new Producer(Int32.Parse(reader["Id"].ToString()),
                                                        reader["Name"].ToString(),
                                                        reader["Email"].ToString(),
                                                        reader["Password"].ToString(),
                                                        perfil.Result);

                            return producer;
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

        public async Task<Producer> Insert(Producer producer)
        {
            try
            {
                using(var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = @"INSERT INTO Producer (
                                                Name,
                                                Email,
                                                Password,
                                                IdPerfil)
                                           VALUES (
                                                @name,
                                                @email,
                                                @password,
                                                @perfil); SELECT scope_Identity();";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("name", producer.Name);
                        cmd.Parameters.AddWithValue("email", producer.Email);
                        cmd.Parameters.AddWithValue("password", producer.Password);
                        cmd.Parameters.AddWithValue("perfil", producer.Perfil.Id);

                        con.Open();
                        var id = cmd.ExecuteScalar().ToString();
                        return await GetByIdAsync(Int32.Parse(id));
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Producer Update(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"DELETE FROM Producer WHERE ID = {id}";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

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
