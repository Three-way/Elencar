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
    public class ProfileReopository : IProfileRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IGenreRepository _genreRepository;

        public ProfileReopository(IConfiguration configuration, IGenreRepository genreRepository)
        {
            _configuration = configuration;
            _genreRepository = genreRepository;
        }



        public async Task<IEnumerable<Profile>> Get()
        {
            return default;
        }

        public async Task<Profile> GetByIdAsync(int id)
        {
            return default;
        }

        public async Task<Profile> Insert(Profile profile)
        {
            return default;
        }

        public async Task<Profile> Update(Profile profile)
        {
            return default;
        }

        public async void Delete(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"DELETE FROM [dbo].[Profile] WHERE ID = {id}";

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

        public async Task<Profile> GetByActorId(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"SELECT * FROM [dbo].[Profile] WHERE User_Id = {id}";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                        return default;
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
