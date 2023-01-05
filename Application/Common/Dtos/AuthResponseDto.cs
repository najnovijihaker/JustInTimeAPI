namespace Application.Common.Dtos
{
    public class AuthResponseDto
    {
        public string StatusMessage { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public string? Token { get; set; }

        public AuthResponseDto(int statusCode, string message)
        {
            StatusMessage = message;
            StatusCode = statusCode;
        }

        public AuthResponseDto(int statusCode, string message, string token)
        {
            StatusMessage = message;
            StatusCode = statusCode;
            Token = token;
        }
    }
}