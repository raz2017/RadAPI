using Moq;
using RadancyAPI.Services;

namespace UnitTests;

public class AccountServiceTests
{
    [Fact]
    public void CreateAccount_ValidUserId_ReturnsNewAccount()
    {
        //Arrange
        var userServiceMock = new Mock<IUserService>();
        userServiceMock.Setup(x => x.UserExists(It.IsAny<Guid>())).Returns(true);

        var target = CreateTarget(userServiceMock);

        //Act
        var result = target.CreateAccount(Guid.NewGuid());

        //Assert
        Assert.Equal(100, result.Balance);
        Assert.NotEqual(Guid.Empty, result.Id);
    }

    private IAccountService CreateTarget(Mock<IUserService> userService)
    {
        return new AccountService(userService.Object);
    }
}