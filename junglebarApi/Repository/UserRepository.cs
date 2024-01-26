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
}
