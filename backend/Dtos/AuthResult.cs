namespace TwitterClone.Api.Dtos;

public class AuthResult
{
    public bool Success { get; set; }
    public AuthResponse? Response { get; set; }
    public string? Error { get; set; }
    public string[]? Errors { get; set; }

    public static AuthResult Successful(AuthResponse response) => new()
    {
        Success = true,
        Response = response
    };

    public static AuthResult Failure(string error) => new()
    {
        Success = false,
        Error = error
    };

    public static AuthResult Failure(string[] errors) => new()
    {
        Success = false,
        Errors = errors
    };
}