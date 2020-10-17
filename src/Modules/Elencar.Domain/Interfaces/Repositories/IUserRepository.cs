using Elencar.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elencar.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> Get();
        Task<User> GetByIdAsync(int id);
        int Insert(User actor);
        Task<User> Update(User actor);
        void Delete(int id);
    }
}
