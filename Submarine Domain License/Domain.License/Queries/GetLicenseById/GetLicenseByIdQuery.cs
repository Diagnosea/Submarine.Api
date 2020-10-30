using System;
using Diagnosea.Submarine.Domain.License.Entities;
using MediatR;

namespace Diagnosea.Submarine.Domain.License.Queries.GetLicenseById
{
    public class GetLicenseByIdQuery : IRequest<LicenseEntity>
    {
        public Guid LicenseId { get; set; }
    }
}