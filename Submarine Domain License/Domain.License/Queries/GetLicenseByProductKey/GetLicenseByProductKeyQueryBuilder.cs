namespace Diagnosea.Submarine.Domain.License.Queries.GetLicenseByProductKey
{
    public class GetLicenseByProductKeyQueryBuilder
    {
        private string _productKey;

        public GetLicenseByProductKeyQueryBuilder WithProductKey(string productKey)
        {
            _productKey = productKey;
            return this;
        }
        
        public GetLicenseByProductKeyQuery Build()
        {
            return new GetLicenseByProductKeyQuery
            {
                ProductKey = _productKey
            };
        }
    }
}