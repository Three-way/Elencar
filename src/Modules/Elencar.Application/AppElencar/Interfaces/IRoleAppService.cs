using Elencar.Application.AppElencar.Input;
using Elencar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Application.AppElencar.Interfaces
{
    public interface IRoleAppService
    {
        Task<IEnumerable<Role>> Get();
        Task<Role> GetByIdAsync(int id);
        Task<Role> Insert(RoleInput roleInput);
        Task<Role> Update(RoleInput roleInput);
        void Delete(int id);

    }
}

