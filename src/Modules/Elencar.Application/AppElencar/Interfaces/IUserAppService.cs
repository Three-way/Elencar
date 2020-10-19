using Elencar.Application.AppElencar.Input;
using Elencar.Application.AppElencar.Input.ObjectValues;
using Elencar.Application.AppElencar.Output;
using Elencar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Application.AppElencar.Interfaces
{
    public interface IUserAppService
    {
        IEnumerable<User> Get();
        Task<User> GetByIdAsync(int id);
        Task<User> Insert(UserInput userInput);
        Task<User> Update(UserInputUpdate userInput);
        void Delete(int id);
    }
}
