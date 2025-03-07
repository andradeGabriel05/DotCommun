using Domain.Entities;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _userDbContext;

    public UserRepository(UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }
    
    public async Task<List<User>> GetAllAsync()
    {
        return await _userDbContext.Users.ToListAsync();
    }

    public Task<User> getUserByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<User> getUserByUserName(string userName)
    {
        throw new NotImplementedException();
    }

    public async Task<User> getUserById(Guid id)
    {
        return await _userDbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<User>> getUsersByStatus(string status)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> getUsersByRole(string role)
    {
        throw new NotImplementedException();
    }

    public async Task<User> CreateAsync(User user)
    {
        await _userDbContext.Users.AddAsync(user);
        await _userDbContext.SaveChangesAsync();
        return user;
    }

    public async Task<int> UpdateAsync(Guid id, User user)
    {
        return await this._userDbContext.Users
            .Where(model => model.Id == id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(x => x.Id, user.Id)
                .SetProperty(x => x.Email, user.Email)
                .SetProperty(x => x.Username, user.Username)
            );
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        return await _userDbContext.Users.Where(model => model.Id == id).ExecuteDeleteAsync();
    }
}