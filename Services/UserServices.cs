using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp6.Models;

namespace WpfApp6.Services;

public class UserService
{
    private static readonly UserService _instance = new UserService();
    public static UserService Instance => _instance;

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