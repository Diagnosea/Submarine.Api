using System;
using Diagnosea.Submarine.Domain.License.Entities;
using MediatR;

namespace Diagnosea.Submarine.Domain.License.Queries.GetLicenseByUserId
{
    public class GetLicenseByUserIdQuery : IRequest<LicenseEntity>
    {
        public Guid UserId { get; set; }
    }
}