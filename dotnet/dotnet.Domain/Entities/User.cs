namespace Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }

    public User(string username, string email)
    {
        Id = Guid.NewGuid();
        Username = username;
        Email = email;
    }
}