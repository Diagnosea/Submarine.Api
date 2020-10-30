using System;

namespace Diagnosea.Submarine.Domain.License.Queries.GetLicenseById
{
    public class GetLicenseByIdQueryBuilder
    {
        private Guid _licenseId;

        public GetLicenseByIdQueryBuilder WithLicenseId(Guid userId)
        {
            _licenseId = userId;
            return this;
        }

        public GetLicenseByIdQuery Build()
        {
            return new GetLicenseByIdQuery
            {
                LicenseId = _licenseId
            };
        }
    }
}