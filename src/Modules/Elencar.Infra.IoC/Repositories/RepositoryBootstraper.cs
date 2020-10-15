using Elencar.Domain.Interfaces.Repositories;
using Elencar.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elencar.Infra.IoC.Repositories
{
    internal class RepositoryBootstraper
    {
        internal void ChildServiceRegister(IServiceCollection services)
        {
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IProfileRepository, ProfileReopository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
        }
    }
}
