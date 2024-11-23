using System;

namespace ProjectManagementApp
{
    public interface IUser
    {
        string Username { get; }
        UserRole Role { get; }        
    }
}