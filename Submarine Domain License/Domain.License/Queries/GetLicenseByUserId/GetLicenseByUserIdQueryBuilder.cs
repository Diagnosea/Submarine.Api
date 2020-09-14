using System;

namespace Diagnosea.Submarine.Domain.License.Queries.GetLicenseByUserId
{
    public class GetLicenseByUserIdQueryBuilder
    {
        private Guid _userId;

        public GetLicenseByUserIdQueryBuilder WithUserId(Guid userId)
        {
            _userId = userId;
            return this;
        }

        public GetLicenseByUserIdQuery Build()
        {
            return new GetLicenseByUserIdQuery
            {
                UserId = _userId
            };
        }
    }
}