﻿using Elencar.Application.AppElencar;
using Elencar.Application.AppElencar.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elencar.Infra.IoC.Application
{
    internal class ApplicationBootstraper
    {
        internal void ChildServiceRegister(IServiceCollection services)
        {
            services.AddScoped<IProducerAppService, ProducerAppService>();
            services.AddScoped<IPerfilAppService, PerfilAppService>();
        }
    }
}