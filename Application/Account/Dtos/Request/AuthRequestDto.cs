using System.ComponentModel.DataAnnotations;

namespace Application.Account.Dtos.Requests
{
    public class AuthRequestDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}