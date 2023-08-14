using RadancyAPI.Domain;

namespace RadancyAPI.Services;

public interface IUserService
{
    public User AddUser(User user);
    public User GetUser(Guid userId);
    public void Update(User userId);
    public void RemoveUser(Guid userId);
    public bool UserExists(Guid  userId);
}