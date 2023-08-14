using RadancyAPI.Domain;
using RadancyAPI.Services;

namespace UnitTests;

public class AccountServiceTest
{
    [Fact]
    public void AddUser_NewUser_ReturnsCreatedUserWithValidId()
    {
        //Arrange
        var target = GetTarget();
        User user = CreateUser();

        //Act
        var result = target.AddUser(user);

        //Assert
        Assert.NotEqual(Guid.Empty, result.Id);

        Assert.Equal(user.FirstName, result.FirstName);
        Assert.Equal(user.LastName, result.LastName);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(user.DOB, result.DOB);
    }

    [Fact]
    public void GetUser_ExistingUser_ReturnsExistingUser()
    {
        //Arrange
        var target = GetTarget();
        User user = CreateUser();
        target.Users.Add(user.Id, user);

        //Act
        var result = target.GetUser(user.Id);

        //Assert
        Assert.NotEqual(Guid.Empty, result.Id);

        Assert.Equal(user.FirstName, result.FirstName);
        Assert.Equal(user.LastName, result.LastName);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(user.DOB, result.DOB);
    }

    [Fact]
    public void RemoveUser_ExistingUser_UserIsDeleted()
    {
        //Arrange
        var target = GetTarget();
        User user = CreateUser();
        target.Users.Add(user.Id, user);

        //Act
        target.RemoveUser(user.Id);

        //Assert
        Assert.False(target.Users.ContainsKey(user.Id));
    }

    private UserService GetTarget()
    {
        return new UserService();
    }

    private User CreateUser()
    {
        return new User("Raziel", "Melchor", "razielmelchor@gmail.com", DateTime.Now);
    }
}