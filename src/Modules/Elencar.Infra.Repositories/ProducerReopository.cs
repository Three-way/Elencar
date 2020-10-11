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

        public ProducerReopository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IEnumerable<Producer> Get()
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var producerList = new List<Producer>();
                    var sqlCmd = @"SELECT * FROM
                                    Producer";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var producer = new Producer(reader["Name"].ToString(),
                                                        reader["Email"].ToString(),
                                                        reader["Password"].ToString(),
                                                        (Perfil)reader["Perfil"]);
                            
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

        public Task<Producer> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Producer Insert(Producer producer)
        {
            throw new NotImplementedException();
        }
    }
}
