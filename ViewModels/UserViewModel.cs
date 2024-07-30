using System.ComponentModel;
using WpfApp6.Services;

namespace WpfApp6.ViewModels;

public class UserViewModel : INotifyPropertyChanged
{
    private readonly UserService _userService;
    private string _currentUserInfo;

    public UserViewModel()
    {
        _userService = UserService.Instance;
        CurrentUserInfo = $"Logged in as: {_userService.CurrentUser?.UserName} ({_userService.CurrentUser?.Role})";
    }

    public string CurrentUserInfo
    {
        get => _currentUserInfo;
        set
        {
            if (_currentUserInfo != value)
            {
                _currentUserInfo = value;
                OnPropertyChanged(nameof(CurrentUserInfo));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}