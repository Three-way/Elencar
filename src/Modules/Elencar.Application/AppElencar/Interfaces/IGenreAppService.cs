using Elencar.Application.AppElencar.Input;
using Elencar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Application.AppElencar.Interfaces
{
    public interface IGenreAppService
    {
        Task<IEnumerable<Genre>> Get();
        Task<Genre> GetByIdAsync(int id);
        Task<Genre> Insert(GenreInput genreInput);
        Task<Genre> Update(GenreInput genreInput);
        void Delete(int id);

    }
}

