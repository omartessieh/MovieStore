using Microsoft.AspNetCore.Identity;

namespace MovieStore.Repositories.Abstract
{
    public interface IRoleRepository
    {
        Task<IEnumerable<IdentityRole>> GetRolesAsync();
        Task<bool> RoleExistsAsync(string roleName);
        Task<bool> CreateRoleAsync(string roleName);
    }
}