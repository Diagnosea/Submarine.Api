namespace Diagnosea.Submarine.Domain.Authentication.Queries.HashText
{
    public class HashTextQueryBuilder
    {
        private string _plainText;

        public HashTextQueryBuilder WithPlainText(string plainText)
        {
            _plainText = plainText;
            return this;
        }

        public HashTextQuery Build()
        {
            return new HashTextQuery
            {
                PlainText = _plainText
            };
        }
    }
}