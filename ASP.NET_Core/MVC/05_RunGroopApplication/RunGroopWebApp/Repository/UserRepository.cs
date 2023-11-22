using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Repository;

public class UserRepository : IUserRepository {

    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) {
        _context = context;
    }

    public bool Add(AppUser user) {
        throw new NotImplementedException();
    }

    public bool Delete(AppUser user) {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AppUser>> GetAllUsers() {
        return await _context.Users.ToListAsync();
    }

    public async Task<AppUser> GetUserById(string id) {
        return await _context.Users.FindAsync(id);
    }

    //public async Task<IEnumerable<IdentityUser>> GetAllUsers() {
    //    return await _context.Users.ToListAsync();
    //}

    //public async Task<IdentityUser> GetUserById(string id) {
    //    return await _context.Users.FindAsync(id);
    //}

    public bool Save() {
        var saved = _context.SaveChanges(true);
        return saved > 0 ? true : false;
    }

    public bool Update(AppUser user) {
        _context.Update(user);
        return Save();
    }
}
