using System;

namespace Diagnosea.Submarine.Domain.User.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FriendlyName { get; set; }
    }
}