using WpfApp6.Models;

namespace WpfApp6.Services;

public class UserService
{
    private static readonly Lazy<UserService> _instance = new Lazy<UserService>(() => new UserService());
    public static UserService Instance => _instance.Value;

    public Login CurrentUser
    {
        get; private set;
    }

    private UserService()
    {
    }

    public void SetCurrentUser(Login user)
    {
        CurrentUser = user;
    }
}