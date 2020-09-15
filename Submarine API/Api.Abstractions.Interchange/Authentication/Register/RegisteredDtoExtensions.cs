﻿using Diagnosea.Submarine.Abstractions.Interchange.Authentication.Register;
using Diagnosea.Submarine.Domain.Authentication.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.Authentication.Register
{
    public static class RegisteredDtoExtensions
    {
        public static RegisteredResponse ToResponse(this RegisteredDto registered)
        {
            return new RegisteredResponse
            {
                UserId = registered.UserId
            };
        }
    }
}