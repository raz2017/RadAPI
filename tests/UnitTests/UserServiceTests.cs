using RadancyAPI.Domain;
using RadancyAPI.Services;

namespace UnitTests;

public class AccountServiceTest
{
    [Fact]
    public void AddUser_AnyUser_ReturnsCreatedUserWithId()
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

    private IUserService GetTarget()
    {
        return new UserService();
    }

    private User CreateUser()
    {
        return new User("Raziel",
            "Melchor",
            "razielmelchor@gmail.com",
            DateTime.Now);
    }
}