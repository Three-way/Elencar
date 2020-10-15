using Elencar.Application.AppElencar.Input;
using Elencar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Application.AppElencar.Interfaces
{
    public interface IProfileAppService
    {
        Task<IEnumerable<Profile>> Get();
        Task<Profile> GetByIdAsync(int id);
        Task<Profile> Insert(ProfileInput profileInput);
        Task<Profile> Update(ProfileInput profileInput);
        void Delete(int id);
    }
}
