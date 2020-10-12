using Elencar.Application.AppElencar.Input;
using Elencar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Application.AppElencar.Interfaces
{
    public interface IProducerAppService
    {
        IEnumerable<Producer> Get();
        Task<Producer> GetbyIdAsync(int id);
        Task<Producer> Insert(ProducerInput producerInput);
        void Delete(int id);
    }
}
