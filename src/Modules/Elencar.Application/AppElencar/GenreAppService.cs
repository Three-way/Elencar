using Elencar.Application.AppElencar.Input;
using Elencar.Application.AppElencar.Interfaces;
using Elencar.Domain.Entities;
using Elencar.Domain.Interfaces.Repositories;
using Marraia.Notifications.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elencar.Application.AppElencar
{
    public class GenreAppService : ControllerBase, IGenreAppService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly ISmartNotification _notification;

        public GenreAppService(IGenreRepository genreRepository, ISmartNotification notification)
        {
            _genreRepository = genreRepository;
            _notification = notification;
        }

        public async Task<IEnumerable<Genre>> Get()
        {
            return await _genreRepository.Get();
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _genreRepository
                            .GetByIdAsync(id)
                            .ConfigureAwait(false);
        }

        public async Task<Genre> Insert(GenreInput genreInput)
        {
           
            var genre = new Genre(genreInput.Name);

            if (genre == default)
            {
                _notification.NewNotificationBadRequest("Invalid Genre!");
                return default;
            }

            return await _genreRepository.Insert(genre);

        }
        public async Task<Genre> Update(GenreInput genreInput)
        {
            var genre = new Genre(genreInput.Name);

            return await _genreRepository.Insert(genre);
        }

        public void Delete(int id)
        {
            _genreRepository.Delete(id);
        }

    }
}
