namespace Diagnosea.Submarine.Domain.Authentication.Queries.HashText
{
    public class HashTextQueryBuilder
    {
        private string _text;

        public HashTextQueryBuilder WithText(string text)
        {
            _text = text;
            return this;
        }

        public HashTextQuery Build()
        {
            return new HashTextQuery
            {
                Text = _text
            };
        }
    }
}