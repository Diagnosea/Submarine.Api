using System;

namespace Diagnosea.Submarine.Abstractions.Interchange.Responses.User
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FriendlyName { get; set; }
    }
}