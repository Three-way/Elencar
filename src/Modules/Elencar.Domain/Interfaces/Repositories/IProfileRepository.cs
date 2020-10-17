using Elencar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Domain.Interfaces.Repositories
{
    public interface IProfileRepository
    {
        Task<IEnumerable<Profile>> Get();
        Task<Profile> GetByIdAsync(int id);
        Task<Profile> GetByActorId(int id);
        Task<Profile> Insert(Profile profile);
        Task<Profile> Update(Profile profile);
        void Delete(int id);

    }
}
