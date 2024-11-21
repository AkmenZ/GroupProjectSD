using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagementApp
{
    public class UsersService
    {
        //List to store users
        private List<User> _users = new();
        //Property to store user repository
        private readonly DataRepository<User> _userRepository;
        //Properties to store dependencies
        private AuthService _authService;
        private LoggerService _loggerService;

        //Constructor, dependency injection
        public UsersService(DataRepository<User> userRepository, LoggerService loggerService, AuthService authService)
        {
            _userRepository = userRepository;
            _loggerService = loggerService;
            _authService = authService;
            _users = _userRepository.GetAll();
        }

        //Add user
        public bool AddUser(User user)
        {
            try
            {
                //Get data
                _users = _userRepository.GetAll();
                //Check if username already exists
                if (_users.Any(u => u.Username == user.Username))
                {
                    throw new Exception($"Username: {user.Username} already exists.");
                }
                //Add user
                _users.Add(user);
                //Save changes
                _userRepository.SaveAll(_users);
                //Log action
                _loggerService.LogAction($"User added: {user.Username}", _authService.CurrentUserRole.ToString(), _authService.CurrentUsername ?? "System");
                //Return true when added
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Add User: {ex.Message}");
            }
        }
        public bool DeleteUser(string username)
        {
            try
            {
                //Get data
                _users = _userRepository.GetAll();
                //Get user
                var user = GetUserByUsername(username);
                //Delete user
                _users.Remove(user);
                //Save changes
                _userRepository.SaveAll(_users);
                //Log action
                _loggerService.LogAction($"User deleted: {username}", _authService.CurrentUserRole.ToString(), _authService.CurrentUsername);
                //Return true when deleted
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Delete User: {ex.Message}");
            }
        }

        public bool ChangePassword(string username, string newPassword)
        {
            try
            {
                //Get data
                _users = _userRepository.GetAll();
                //Get user
                var user = GetUserByUsername(username);                
                //Change password
                user.SetPassword(newPassword);
                //Save changes
                _userRepository.SaveAll(_users);
                //Log action
                _loggerService.LogAction($"Password changed for: {username}", _authService.CurrentUserRole.ToString(), _authService.CurrentUsername);
                //Return true when password changed
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Change Password: {ex.Message}");
            }
        }

        //Get user by username
        public User GetUserByUsername(string username)
        {
            try
            {
                var user = _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
                if (user == null)
                {
                    throw new Exception($"User with username {username} not found.");
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error get user by username: {ex.Message}");
            }
        }

        //Get manager by username
        public User GetManagerByUsername(string username)
        {
            try
            {
                var user = _users.FirstOrDefault(u => u.Role == UserRole.Manager && u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
                if (user == null)
                {
                    throw new Exception($"Manager with username {username} not found.");
                }                
                
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error get Manager by username: {ex.Message}");
            }
        }

        //Get users by role, return as read-only list
        public IReadOnlyList<User> GetUsersByRole(UserRole role)
        {
            try
            {
                return _users.Where(u => u.Role == role).ToList().AsReadOnly();
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error get users by role: {ex.Message}");
            }
        }

        //Get all users, return as read-only list
        public IReadOnlyList<User> GetAllUsers()
        {
            try
            {
                return _users.OrderBy(u => u.Role).ToList().AsReadOnly();
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error get all users: {ex.Message}");
            }
        }
    }
}