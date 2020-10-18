using Elencar.Domain.Entities;

namespace Elencar.Application.AppElencar.Output
{
    public class UserViewModel
    {

        public UserViewModel(int id, string name, string email, Role role)
        {
            Id = id;
            Name = name;
            Email = email;
            Role = role;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
