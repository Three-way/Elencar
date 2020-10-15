using Elencar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Domain.Interfaces.Repositories
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> Get();
        Task<Genre> GetByIdAsync(int id);
        Task<Genre> Insert(Genre genre);
        Task<Genre> Update(Genre genre);
        void Delete(int id);
    }
}
