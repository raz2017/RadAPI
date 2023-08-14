using RadancyAPI.Domain;

namespace RadancyAPI.Services;

public class AccountService : IAccountService
{
    private readonly IUserService _userService;
    private readonly Dictionary<Guid, Dictionary<Guid,Account>> _accounts;

    public AccountService(IUserService userService)
    {
        _userService = userService;
        _accounts = new Dictionary<Guid, Dictionary<Guid, Account>>();
    }

    public Account GetAccount(Guid userId, Guid accountId)
    {
        ValidateUserAccount(userId, accountId);

        return _accounts[userId][accountId];
    }

    public Account CreateAccount(Guid userId)
    {
        ValidateUser(userId);

        var newAccount = new Account();

        if (_accounts.ContainsKey(userId))
        {
            _accounts[userId].Add(newAccount.Id, newAccount);
        }
        else
        {
            _accounts.Add(userId, new Dictionary<Guid, Account>() { { newAccount.Id, newAccount } });
        }

        return newAccount;
    }

    public Account UpdateAccount(Guid userId, Guid accountId, AccountAction action, double amount)
    {
        ValidateUserAccount(userId, accountId);

        var account = _accounts[userId][accountId];

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

        _accounts[userId].Remove(accountId);
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

        var userAccountExists = _accounts.ContainsKey(userId) && _accounts[userId].ContainsKey(accountId);

        if (!userAccountExists) throw new ArgumentException($"AccountId '{accountId}' for UserId '{userId}' does not exist.");
    }
}