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
    public class ProfileReopository : IProfileRepository
    {
        private readonly IConfiguration _configuration;

        public ProfileReopository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        

        public async Task<IEnumerable<Profile>> Get()
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var profileList = new List<Profile>();

                    var sqlCmd = $@"SELECT p.id as profileId, p.bio, p.fee, u.name, u.email, g.description
                                            FROM Profile p
                                            JOIN User u on u.id = p.user_id
                                            JOIN Genre g on g.id = p.genre_id
                                            WHERE p.status = 1 AND u.status = 1";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                        while (reader.Read())
                        {
                            var profile = new Profile()
                            {
                                Id = Int32.Parse(reader["profileId"].ToString()),
                                Bio = reader["bio"].ToString(),
                                Fee = Decimal.Parse(reader["fee"].ToString()),
                                Actor = new Actor()
                                {
                                    Name = reader["name"].ToString(),
                                    Email = reader["email"].ToString(),
                                },
                                Genre = new Genre()
                                {
                                    Description = reader["description"].ToString(),
                                }
                            };

                            profileList.Add(profile);
                        }
                        return profileList;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Profile> GetByIdAsync(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"SELECT p.id as profileId, p.bio, p.fee, u.name, u.email, g.description
                                            FROM Profile p
                                            JOIN User u on u.id = p.user_id and u.status = 1
                                            JOIN Genre g on g.id = p.genre_id
                                            WHERE Id = {id} AND p.status = 1";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                        while (reader.Read())
                        {
                            var profile = new Profile()
                            {
                                Id = Int32.Parse(reader["profileId"].ToString()),
                                Bio = reader["bio"].ToString(),
                                Fee = Decimal.Parse(reader["fee"].ToString()),
                                Actor = new Actor()
                                {
                                    Name = reader["name"].ToString(),
                                    Email = reader["email"].ToString(),
                                },
                                Genre = new Genre()
                                {
                                    Description = reader["description"].ToString(),
                                }
                            };
                            

                            return profile;
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

        public async Task<Profile> Insert(Profile profile)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = @"INSERT INTO Profile (
                                                Bio,
                                                Fee,
                                                User_Id,
                                                Genre_Id,
                                                Status,
                                                CreatedAt,
                                                UpdatedAt)
                                           VALUES (
                                                @bio,
                                                @fee,
                                                @userId,
                                                @genreId,
                                                1,
                                                GetDate(),
                                                GetDate(),
                                                );SELECT scope_Identity();";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("bio", profile.Bio);
                        cmd.Parameters.AddWithValue("fee", profile.Fee);
                        cmd.Parameters.AddWithValue("userId", profile.Actor.Id);
                        cmd.Parameters.AddWithValue("genreId", profile.Genre.Id);

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

        public async Task<Profile> Update(Profile profile)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = $@"UPDATE Profile set
                                                Bio = @bio,
                                                Fee = @fee,
                                                Genre_Id = @genreId
                                    WHERE id = {profile.Id}";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("Bio", profile.Bio);
                        cmd.Parameters.AddWithValue("Fee", profile.Fee);
                        cmd.Parameters.AddWithValue("genreId", profile.Genre.Id);

                        con.Open();
                        return await GetByIdAsync(profile.Id);
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
                    var sqlCmd = $@"DELETE FROM Profile WHERE ID = {id}";

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