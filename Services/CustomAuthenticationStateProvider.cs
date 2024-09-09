using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly Dictionary<string, string> _users = new()
    {
        { "user1", "password1" }, // Usuario de ejemplo
        { "user2", "password2" }  // Otro usuario de ejemplo
    };

    private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();
        if (_currentUser.Identity.IsAuthenticated)
        {
            identity = (ClaimsIdentity)_currentUser.Identity;
        }
        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
    }

    public Task<bool> ValidateUser(string username, string password)
    {
        if (_users.TryGetValue(username, out var correctPassword) && correctPassword == password)
        {
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "User") // Puedes agregar roles si es necesario
            }, "custom"));
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public void Logout()
    {
        _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
