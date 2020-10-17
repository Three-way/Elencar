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

        public async Task<IEnumerable<Actor>> Get()
        {
            return default;
        }

        public async Task<Actor> GetByIdAsync(int id)
        {
            return default;
        }

        public async Task<Actor> Insert(Actor actor)
        {
            return default;
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
                    var sqlCmd = $@"DELETE FROM [dbo].[User] WHERE ID = {id}";

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
            return default;
        }

    }
}
