using Elencar.Application.AppElencar.Input;
using Elencar.Application.AppElencar.Interfaces;
using Elencar.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Application.AppElencar
{
    public class UserAppService : ControllerBase, IUserAppService
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> Insert(UserInput userInput)
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(UserInput userInput)
        {
            throw new NotImplementedException();
        }
    }
}
