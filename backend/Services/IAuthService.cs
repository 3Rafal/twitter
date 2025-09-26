using TwitterClone.Api.Dtos;

namespace TwitterClone.Api.Services;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(RegisterRequest request);
    Task<AuthResult> LoginAsync(LoginRequest request);
    Task<AuthResult> RefreshTokenAsync(RefreshTokenRequest request);
    Task LogoutAsync(string accessToken);
}