﻿using Elencar.Domain.Entities;
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
    public class GenreRepository : IGenreRepository
    {
        private readonly IConfiguration _configuration;

        public GenreRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Genre>> Get()
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var genreList = new List<Genre>();
                    var sqlCmd = $@"SELECT * FROM [dbo].[Genre]";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                        while (reader.Read())
                        {

                            var genre = new Genre(Int32.Parse(reader["userId"].ToString()), reader["description"].ToString());
                            


                            genreList.Add(genre);
                        }
                        return genreList;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"SELECT * FROM [dbo].[Genre]                                           
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

                            var genre = new Genre(id, reader["name"].ToString());

                            return genre;
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

        public async Task<Genre> Insert(Genre genre)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = @"INSERT INTO Genre (
                                                Description,
                                                CreatedAt)
                                           VALUES (
                                                @description,
                                                GetDate(),
                                                );SELECT scope_Identity();";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("description", genre.Description);

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

        public async Task<Genre> Update(Genre genre)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"UPDATE Genre set
                                                Description = @description,
                                    WHERE id = {genre.Id}";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("description", genre.Description);

                        con.Open();
                        return await GetByIdAsync(genre.Id);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async void Delete(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"DELETE FROM Genre WHERE ID = {id}";

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
