namespace Diagnosea.Submarine.Domain.Authentication.Queries.CompareHashText
{
    public class CompareHashTextQueryBuilder
    {
        private string _hash;
        private string _text;

        public CompareHashTextQueryBuilder WithHash(string hash)
        {
            _hash = hash;
            return this;
        }

        public CompareHashTextQueryBuilder WithText(string text)
        {
            _text = text;
            return this;
        }
        
        public CompareHashTextQuery Build()
        {
            return new CompareHashTextQuery
            {
                Hash = _hash,
                Text = _text
            };
        }
    }
}