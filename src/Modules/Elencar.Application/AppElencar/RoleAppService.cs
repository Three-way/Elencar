using Elencar.Application.AppElencar.Input;
using Elencar.Application.AppElencar.Interfaces;
using Elencar.Domain.Entities;
using Elencar.Domain.Interfaces.Repositories;
using Marraia.Notifications.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elencar.Application.AppElencar
{
    public class RoleAppService : ControllerBase, IRoleAppService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ISmartNotification _notification;

        public RoleAppService(IRoleRepository roleRepository, ISmartNotification notification)
        {
            _roleRepository = roleRepository;
            _notification = notification;
        }

        public async Task<IEnumerable<Role>> Get()
        {
            return await _roleRepository.Get();
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await _roleRepository
                            .GetByIdAsync(id)
                            .ConfigureAwait(false);
        }

        public async Task<Role> Insert(RoleInput roleInput)
        {
           
            var role = new Role(roleInput.Name);

            if (role == default)
            {
                _notification.NewNotificationBadRequest("Invalid Role!");
                return default;
            }

            return await _roleRepository.Insert(role);

        }
        public async Task<Role> Update(RoleInput roleInput)
        {
            var role = new Role(roleInput.Name);

            return await _roleRepository.Insert(role);
        }

        public void Delete(int id)
        {
            _roleRepository.Delete(id);
        }

    }
}
