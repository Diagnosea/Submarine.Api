﻿using Diagnosea.Submarine.Abstractions.Interchange.Authentication;
using Diagnosea.Submarine.Domain.Authentication.Dtos;

namespace Diagnosea.Submarine.Api.Extensions
{
    public static class AuthenticatedDtoExtensions
    {
        public static AuthenticatedResponse ToResponse(this AuthenticatedDto authenticated)
        {
            return new AuthenticatedResponse
            {
                BearerToken = authenticated.BearerToken
            };
        }
    }
}