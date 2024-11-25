using System;

namespace ProjectManagementApp
{
    public interface ITeamMember : IUser
    {
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
        string Phone { get; }        
    }
}