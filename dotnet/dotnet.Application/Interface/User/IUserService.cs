using Domain.Entities;

public interface IUserService
{
    Task<List<User>> GetAllAsync();
    Task<User> getUserByEmail(string email);
    Task<User> getUserByUserName(string userName);
    Task<User> getUserById(Guid id);
    Task<List<User>> getUsersByStatus(string status);
    Task<List<User>> getUsersByRole(string role);
    // Task<List<User>> getUsersByStatus(string status, int page, int pageSize, int pageOffset);

    
    Task<User> CreateAsync(User user);
    
    Task<int> UpdateAsync(Guid id, User user);
    
    Task<int> DeleteAsync(Guid id);
}
