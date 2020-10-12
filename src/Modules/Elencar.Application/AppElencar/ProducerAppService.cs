using Elencar.Application.AppElencar.Input;
using Elencar.Application.AppElencar.Interfaces;
using Elencar.Domain.Entities;
using Elencar.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Application.AppElencar
{
    public class ProducerAppService : ControllerBase, IProducerAppService
    {
        private readonly IProducerRepository _producerRepository;
        private readonly IPerfilRepository _perfilRepository;


        public ProducerAppService(IProducerRepository producerRepository, IPerfilRepository perfilRepository)
        {
            _producerRepository = producerRepository;
            _perfilRepository = perfilRepository;
        }

        public IEnumerable<Producer> Get()
        {
            return _producerRepository.Get();
        }

        public async Task<Producer> GetbyIdAsync(int id)
        {
            return await _producerRepository
                            .GetByIdAsync(id)
                            .ConfigureAwait(false);
        }

        public async Task<Producer> Insert(ProducerInput producerInput)
        {
            if(!producerInput.IsValid())
            {
                return default;
            }

            var perfil = _perfilRepository.GetByIdAsync(producerInput.IdPerfil).Result;

            if (perfil == null)
            {
                return default;
            }
            var producer = new Producer(producerInput.Name, producerInput.Email
                                        , producerInput.Password,  perfil);

            if (!producer.IsValid())
            {
                return default;
            }

            return await _producerRepository.Insert(producer);

        }

        public void Delete(int id)
        {
            _producerRepository.Delete(id);
        }

    }
}
