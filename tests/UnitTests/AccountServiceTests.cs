using Moq;
using RadancyAPI.Domain;
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

    [Fact]
    public void GetAccount_ValidUserIdAndAccountId_ReturnsAssociatedAccount()
    {
        //Arrange
        var userServiceMock = new Mock<IUserService>();
        userServiceMock.Setup(x => x.UserExists(It.IsAny<Guid>())).Returns(true);
        var target = CreateTarget(userServiceMock);

        //Getting account presupposes account exists
        var userId = Guid.NewGuid();
        var account = new Account();
        target.Accounts.Add(userId, new() { { account.Id, account } });

        //Act
        var result = target.GetAccount(userId, account.Id);

        //Assert
        Assert.Equal(100, result.Balance);
        Assert.Equal(account.Id, result.Id);
    }

    [Fact]
    public void DeleteAccount_ValidUserIdAndAccountId_AccountIsDeleted()
    {
        //Arrange
        var userServiceMock = new Mock<IUserService>();
        userServiceMock.Setup(x => x.UserExists(It.IsAny<Guid>())).Returns(true);

        var target = CreateTarget(userServiceMock);

        //Deleting account presupposes account exists
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        target.Accounts.Add(userId, new() { { accountId, new Account() } });

        //Act
        target.RemoveAccount(userId, accountId);

        //Assert
        Assert.Empty(target.Accounts[userId]);
    }

    [Fact]
    public void UpdateAccount_UserDeposit_ReturnsUpdatedAccount()
    {
        //Arrange
        var userServiceMock = new Mock<IUserService>();
        userServiceMock.Setup(x => x.UserExists(It.IsAny<Guid>())).Returns(true);

        var target = CreateTarget(userServiceMock);

        //Update to accounts presupposes account already exists
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        target.Accounts.Add(userId, new(){{accountId, new Account(150)}});

        //Act
        var result = target.UpdateAccount(userId, accountId, AccountAction.Deposit, 100);

        //Assert
        Assert.Equal(250, result.Balance);
        Assert.NotEqual(accountId, result.Id);
    }

    [Fact]
    public void UpdateAccount_UserWithdrawal_ReturnsUpdatedAccount()
    {
        //Arrange
        var userServiceMock = new Mock<IUserService>();
        userServiceMock.Setup(x => x.UserExists(It.IsAny<Guid>())).Returns(true);

        var target = CreateTarget(userServiceMock);

        //Update to accounts presupposes account already exists
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        target.Accounts.Add(userId, new() { { accountId, new Account(1000) } });

        //Act
        var result = target.UpdateAccount(userId, accountId, AccountAction.Withdraw, 100);

        //Assert
        Assert.Equal(900, result.Balance);
        Assert.NotEqual(accountId, result.Id);
    }

    [Fact]
    public void UpdateAccount_UserBalanceWouldBeLessThan100_ThrowsValidationException()
    {
        //Arrange
        var userServiceMock = new Mock<IUserService>();
        userServiceMock.Setup(x => x.UserExists(It.IsAny<Guid>())).Returns(true);

        var target = CreateTarget(userServiceMock);

        //Update to accounts presupposes account already exists
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        target.Accounts.Add(userId, new() { { accountId, new Account(200) } });

        //Act + Assert
        var exception = Assert.Throws<ArgumentException>(() => target.UpdateAccount(userId, accountId, AccountAction.Withdraw, 101));

        Assert.Equal("An account cannot have less than $100 at any time.", exception.Message);
    }

    [Fact]
    public void UpdateAccount_UserWithdrawsMoreThan90PercentOfBalance_ThrowsValidationException()
    {
        //Arrange
        var userServiceMock = new Mock<IUserService>();
        userServiceMock.Setup(x => x.UserExists(It.IsAny<Guid>())).Returns(true);

        var target = CreateTarget(userServiceMock);

        //Update to accounts presupposes account already exists
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        target.Accounts.Add(userId, new() { { accountId, new Account(2000) } });

        //Act + Assert
        var exception = Assert.Throws<ArgumentException>(() => target.UpdateAccount(userId, accountId, AccountAction.Withdraw, 1850));

        Assert.Equal("A user cannot withdraw more than 90% of their total balance from an account in a single transaction.", exception.Message);
    }

    [Fact]
    public void UpdateAccount_UserDepositsMoreThan10000_ThrowsValidationException()
    {
        //Arrange
        var userServiceMock = new Mock<IUserService>();
        userServiceMock.Setup(x => x.UserExists(It.IsAny<Guid>())).Returns(true);

        var target = CreateTarget(userServiceMock);

        //Update to accounts presupposes account already exists
        var userId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        target.Accounts.Add(userId, new() { { accountId, new Account() } });

        //Act + Assert
        var exception = Assert.Throws<ArgumentException>(() => target.UpdateAccount(userId, accountId, AccountAction.Deposit, 10001));

        Assert.Equal("A user cannot deposit more than $10,000 in a single transaction.", exception.Message);
    }

    private AccountService CreateTarget(Mock<IUserService> userService)
    {
        return new AccountService(userService.Object);
    }
}