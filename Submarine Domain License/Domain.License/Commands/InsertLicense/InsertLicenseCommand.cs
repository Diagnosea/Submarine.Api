using System;
using System.Collections.Generic;
using MediatR;

namespace Diagnosea.Submarine.Domain.License.Commands.InsertLicense
{
    public class InsertLicenseCommand : IRequest
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public Guid UserId { get; set; }
        public IList<InsertLicenseProductCommand> Products { get; set; }
    }
}