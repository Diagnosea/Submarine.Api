using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.License.Dtos;

namespace Diagnosea.Submarine.Domain.Instructors.License
{
    public interface ILicenseInstructor
    {
        Task<CreatedLicenseDto> CreateAsync(CreateLicenseDto createLicense, CancellationToken token);
    }
}