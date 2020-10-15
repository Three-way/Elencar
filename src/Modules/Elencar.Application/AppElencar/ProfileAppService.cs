using Elencar.Application.AppElencar.Input;
using Elencar.Application.AppElencar.Interfaces;
using Elencar.Domain.Entities;
using Elencar.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Application.AppElencar
{
    public class ProfileAppService : ControllerBase, IProfileAppService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IActorRepository _actorRepository;


        public ProfileAppService(IProfileRepository profileRepository, IActorRepository actorRepository, IGenreRepository genreRepository)
        {
            _profileRepository = profileRepository;
            _genreRepository = genreRepository;
            _actorRepository = actorRepository;
        }

        public async Task<IEnumerable<Profile>> Get()
        {
            return await _profileRepository.Get();
        }

        public async Task<Profile> GetByIdAsync(int id)
        {
            return await _profileRepository
                            .GetByIdAsync(id)
                            .ConfigureAwait(false);
        }

        public async Task<Profile> Insert(ProfileInput profileInput)
        {
            var genre = await _genreRepository.GetByIdAsync(profileInput.GenreId);
            var actor = await _actorRepository.GetByIdAsync(profileInput.ActorId);
            var profile = new Profile() { Bio = profileInput.Bio, Fee = profileInput.Fee, Genre = genre, Actor = actor };


            return await _profileRepository.Insert(profile);

        }
        public async Task<Profile> Update(ProfileInput profileInput)
        {
            var genre = await _genreRepository.GetByIdAsync(profileInput.GenreId);

            var profile = new Profile() { Bio = profileInput.Bio, Fee = profileInput.Fee, Genre = genre };

            return await _profileRepository.Insert(profile);
        }

        public void Delete(int id)
        {
            _profileRepository.Delete(id);
        }

    }
}
