namespace junglebarApi.Service;

using junglebarApi.Models;
using junglebarApi.Repository;
using System.Collections.Generic;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public User GetUserByEmailAndPassword(string email, string password)
    {
        User user = _userRepository.GetUserByEmailAndPassword(email, password);
    
        if (user != null)
        {
            if (BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return user;
            }
        }
    
        return null;
    }

    
    public void AddUser(User user)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.Password = hashedPassword;
    
        _userRepository.AddUser(user);
    }
    
    public void DeleteUserById(int id)
    {
        _userRepository.DeleteUserById(id);
    }

    public void DeleteAllUsers()
    {
        _userRepository.DeleteAllUsers();
    }

    public void UpdateUserById(int id, User updatedUser)
    {
        _userRepository.UpdateUserById(id, updatedUser);
    }
    
    public User GetUserById(int id)
    {
        return _userRepository.GetUserById(id);
    }
}
