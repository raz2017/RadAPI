using RadancyAPI.Domain;

namespace RadancyAPI.Services;

public class AccountService : IAccountService
{
    private readonly IUserService _userService;
    public readonly Dictionary<Guid, Dictionary<Guid,Account>> Accounts;

    public AccountService(IUserService userService)
    {
        _userService = userService;
        Accounts = new Dictionary<Guid, Dictionary<Guid, Account>>();
    }

    public Account GetAccount(Guid userId, Guid accountId)
    {
        ValidateUserAccount(userId, accountId);

        return Accounts[userId][accountId];
    }

    public Account CreateAccount(Guid userId)
    {
        ValidateUser(userId);

        var newAccount = new Account();

        if (Accounts.ContainsKey(userId))
        {
            Accounts[userId].Add(newAccount.Id, newAccount);
        }
        else
        {
            Accounts.Add(userId, new() { { newAccount.Id, newAccount } });
        }

        return newAccount;
    }

    public Account UpdateAccount(Guid userId, Guid accountId, AccountAction action, double amount)
    {
        ValidateUserAccount(userId, accountId);

        var account = Accounts[userId][accountId];

        switch (action)
        {
            case AccountAction.Deposit:
                account.Deposit(amount);
                break;
            case AccountAction.Withdraw:
                account.Withdraw(amount);
                break;
            default:
                throw new ArgumentException("AccountAction is not valid");
        }

        return account;
    }

    public void RemoveAccount(Guid userId, Guid accountId)
    {
        ValidateUserAccount(userId, accountId);

        Accounts[userId].Remove(accountId);
    }

    private void ValidateUser(Guid userId)
    {
        var userExists = _userService.UserExists(userId);

        if (!userExists) throw new ArgumentException($"UserId '{userId}' does not exist.");
    }

    private void ValidateUserAccount(Guid userId, Guid accountId)
    {
        var userExists = _userService.UserExists(userId);

        if (!userExists) throw new ArgumentException($"UserId '{userId}' does not exist.");

        var userAccountExists = Accounts.ContainsKey(userId) && Accounts[userId].ContainsKey(accountId);

        if (!userAccountExists) throw new ArgumentException($"AccountId '{accountId}' for UserId '{userId}' does not exist.");
    }
}