using RadancyAPI.Domain;

namespace RadancyAPI.Services;

public class UserService : IUserService
{
    private Dictionary<Guid, User> Users;

    public UserService()
    {
        Users = new Dictionary<Guid, User>();

    }
    public User AddUser(User user)
    {
        var userExists = UserExists(user.Id);

        if (userExists) throw new ArgumentException("User already exists.");

        Users.Add(user.Id, user);

        return user;
    }

    public User GetUser(Guid userId)
    {
        var userExists = UserExists(userId);

        if (!userExists) throw new ArgumentException("User does not exist.");

        return Users[userId];
    }

    public void Update(User user)
    {
        var userExists = UserExists(user.Id);

        if (!userExists) throw new ArgumentException("User does not exist.");

        Users[user.Id] = user;
    }

    public void RemoveUser(Guid userId)
    {
        var userExists = UserExists(userId);

        if (!userExists) throw new ArgumentException("User does not exist.");

        Users.Remove(userId);
    }

    public bool UserExists(Guid userId)
    {
        var userExists = Users.ContainsKey(userId);

        return userExists;
    }
}