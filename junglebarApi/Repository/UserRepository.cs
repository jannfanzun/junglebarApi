namespace junglebarApi.Repository;

using junglebarApi.Data;
using junglebarApi.Models;
using System.Collections.Generic;
using System.Linq;

public class UserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }
    
    public User GetUserByEmailAndPassword(string email, string password)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }
    
    public void DeleteUserById(int id)
    {
        var user = _context.Users.Find(id);

        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }

    public void DeleteAllUsers()
    {
        _context.Users.RemoveRange(_context.Users);
        _context.SaveChanges();
    }

    public void UpdateUserById(int id, User updatedUser)
    {
        var user = _context.Users.Find(id);

        if (user != null)
        {
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;

            _context.SaveChanges();
        }
    }
    
    public User GetUserById(int id)
    {
        return _context.Users.Find(id);
    }
}
