using System.ComponentModel.DataAnnotations;

namespace Application.Account.Dtos.Requests
{
    public class CreateAccountRequestDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required, MinLength(8), Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public bool StudentAccount { get; set; } = false;
    }
}