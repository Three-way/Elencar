using Elencar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Domain.Interfaces.Repositories
{
    public interface IProducerRepository
    {
        IEnumerable<Producer> Get();
        Task<Producer> GetByIdAsync(int id);
        Task<Producer> Insert(Producer producer);
        void Delete(int id);
        Producer Update(int id);
    }
}
