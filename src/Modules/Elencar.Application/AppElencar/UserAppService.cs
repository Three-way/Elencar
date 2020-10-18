using Elencar.Application.AppElencar.Input;
using Elencar.Application.AppElencar.Interfaces;
using Elencar.Application.AppElencar.Output;
using Elencar.Domain.Entities;
using Elencar.Domain.Interfaces.Repositories;
using Marraia.Notifications.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elencar.Application.AppElencar
{
    public class UserAppService : ControllerBase, IUserAppService
    {

        private readonly IUserRepository _userRepository;
        private readonly ISmartNotification _notification;
        public UserAppService(ISmartNotification notification,
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _notification = notification;
        }
        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }

        public IEnumerable<User> Get()
        {
            return _userRepository.Get();
        }

        public Task<User> GetByIdAsync(int id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public async Task<User> Insert(UserInput userInput)
        {
            var user = new User(userInput.Name, userInput.Email, userInput.Password , new Role(userInput.Role));
            if (!user.IsValidEmail(userInput.Email))
            {
                _notification.NewNotificationBadRequest("Insira um e-mail válido!");
                return default;
            };
            if (!user.IsValid())
            {
                _notification.NewNotificationBadRequest("Dados do usuário são obrigatórios");
                return default;
            };
            var id =  await _userRepository.Insert(user);
            return await _userRepository.GetByIdAsync(id);

        }


        public async Task<User> Update(UserInput userInput)
        {
            var user = new User(userInput.Name, userInput.Email, userInput.Password, userInput.Status, new Role(userInput.Role));
            if (!user.IsValidEmail(userInput.Email))
            {
                _notification.NewNotificationBadRequest("Insira um e-mail válido!");
                return default;
            };
            if (!user.IsValid())
            {
                _notification.NewNotificationBadRequest("Insira dados válidos!");
                return default;
            };
            var id = await _userRepository.Update(user);

            return await _userRepository.GetByIdAsync(id);
        }
    }
}
