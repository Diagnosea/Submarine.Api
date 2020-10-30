using System;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.License.Dtos;

namespace Diagnosea.Submarine.Domain.Instructors.License
{
    public interface ILicenseInstructor
    {
        Task<LicenseDto> GetByIdAsync(Guid id, CancellationToken token);
        Task<CreatedLicenseDto> CreateAsync(CreateLicenseDto createLicense, CancellationToken token);
    }
}