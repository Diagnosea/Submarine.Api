using System.ComponentModel.DataAnnotations;

namespace Diagnosea.Submarine.Api.Models.Request
{
    public class AuthenticationRequestModel
    {
        [Required]
        public string PrivateSigningKey { get; set; }
        [Required]
        public string Audience { get; set; }
        [Required] 
        public string Issuer { get; set; }
    }
}