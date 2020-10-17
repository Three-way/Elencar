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
    class UserAppService : ControllerBase, IProfileAppService
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Profile>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Profile> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Profile> Insert(ProfileInput profileInput)
        {
            throw new NotImplementedException();
        }

        public Task<Profile> Update(ProfileInput profileInput)
        {
            throw new NotImplementedException();
        }
    }
}
