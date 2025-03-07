using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<List<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public Task<User> getUserByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<User> getUserByUserName(string userName)
    {
        throw new NotImplementedException();
    }

    public async Task<User>  getUserById(Guid id)
    {
        return await _userRepository.getUserById(id);
    }

    public Task<List<User>> getUsersByRole(string role)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> getUsersByStatus(string status)
    {
        throw new NotImplementedException();
    }

    public async Task<User> CreateAsync(User user)
    {
        return await _userRepository.CreateAsync(user);
    }

    public async Task<int> UpdateAsync(Guid id, User user)
    {
        return await _userRepository.UpdateAsync(id, user);
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        return await _userRepository.DeleteAsync(id);
    }
}