using Elencar.Application.AppElencar.Interfaces;
using Elencar.Domain.Entities;
using Elencar.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Application.AppElencar
{
    public class PerfilAppService : ControllerBase, IPerfilAppService
    {
        private readonly IPerfilRepository _perfilRepository;

        public PerfilAppService(IPerfilRepository perfilRepository)
        {
            _perfilRepository = perfilRepository;
        }

        public async Task<Perfil> GetbyIdAsync(int id)
        {
            var perfil = await _perfilRepository
                            .GetByIdAsync(id)
                            .ConfigureAwait(false);
            if (perfil == null)
            {
                return default;
            }
            return perfil;
        }
    }
}
