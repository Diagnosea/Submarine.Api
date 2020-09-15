﻿using Diagnosea.Submarine.Abstractions.Interchange.Authentication.Register;
using Diagnosea.Submarine.Domain.Authentication.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.Authentication.Register
{
    public static class RegisterRequestExtensions
    {
        public static RegisterDto ToDto(this RegisterRequest register)
        {
            return new RegisterDto
            {
                EmailAddress = register.EmailAddress,
                PlainTextPassword = register.Password,
                UserName = register.UserName,
                FriendlyName = register.FriendlyName
            };
        }
    }
}