using Elencar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Application.AppElencar.Interfaces
{
    public interface IPerfilAppService
    {
        Task<Perfil> GetbyIdAsync(int id);
    }
}
