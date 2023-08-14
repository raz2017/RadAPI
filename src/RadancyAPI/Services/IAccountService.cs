using RadancyAPI.Domain;

namespace RadancyAPI.Services;

public interface IAccountService
{
    public Account GetAccount(Guid userId, Guid accountId);
    public Account CreateAccount(Guid userId);
    public void RemoveAccount(Guid userId, Guid accountId);
    public Account UpdateAccount(Guid userId, Guid accountId, AccountAction action, double amount);
}