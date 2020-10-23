using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.License.Dtos;

namespace Diagnosea.Submarine.Domain.Instructors.License
{
    public interface ILicenseInstructor
    {
        Task CreateAsync(CreateLicenseDto createLicense);
    }
}