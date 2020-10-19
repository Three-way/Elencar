using Elencar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Domain.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> Get();
        Task<Role> GetByIdAsync(int id);
        Task<Role> Insert(Role role);
        Task<Role> Update(Role role);
        void Delete(int id);
    }
}
