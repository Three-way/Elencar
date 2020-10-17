using Elencar.Application.AppElencar.Input;
using Elencar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Application.AppElencar.Interfaces
{
    public interface IUserAppService
    {
        Task<IEnumerable<User>> Get();
        Task<User> GetByIdAsync(int id);
        Task<User> Insert(UserInput userInput);
        Task<User> Update(UserInput userInput);
        void Delete(int id);
    }
}
