using Elencar.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elencar.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> Get();
        Task<User> GetByIdAsync(int id);
        Task<int> Insert(User actor);
        Task<User> GetByEmailAsync(string email);
        Task<User> Update(User actor);
        void Delete(int id);
    }
}
