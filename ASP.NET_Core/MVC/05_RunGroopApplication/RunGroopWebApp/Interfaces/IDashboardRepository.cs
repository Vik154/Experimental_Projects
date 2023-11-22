using Microsoft.AspNetCore.Identity;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Interfaces;

public interface IDashboardRepository {
    Task<List<Race>> GetAllUserRaces();
    Task<List<Club>> GetAllUserClubs();
    Task<AppUser> GetUserById(string id);
    Task<AppUser> GetByIdNoTracking(string id);
    //Task<IdentityUser> GetUserById(string id);
    //Task<IdentityUser> GetByIdNoTracking(string id);
    bool Update(AppUser user);
    bool Save();
}
