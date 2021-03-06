﻿using Diagnosea.Submarine.Domain.License.Commands.InsertLicense;
using Diagnosea.Submarine.Domain.License.Entities;

namespace Diagnosea.Submarine.Domain.License.Extensions
{
    public static class InsertLicenseCommandExtensions
    {
        public static LicenseEntity ToEntity(this InsertLicenseCommand command)
        {
            return new LicenseEntity
            {
                Id = command.Id,
                Key = command.Key,
                Created = command.Created,
                UserId = command.UserId
            };
        }
    }
}