namespace RadancyAPI.Domain;

public record Account
{
    public Guid Id { get; set; }
    public double Balance { get; set; }

    public Account(double balance = 100)
    {
        if (balance < 100) throw new ArgumentException("An account cannot have less than $100 at any time.");

        Id = Guid.NewGuid();
        Balance = balance;
    }

    public void Deposit(double amount)
    {
        ValidateDeposit(amount);

        Balance += amount;
    }

    public void Withdraw(double amount)
    {
        ValidateWithdrawal(amount);

        Balance -= amount;
    }
    private void ValidateDeposit(double amount)
    {
        if (amount > 10000)
        {
            throw new ArgumentException("A user cannot deposit more than $10,000 in a single transaction.");
        }
    }

    private void ValidateWithdrawal(double amount)
    {
        if ((Balance - amount) < 100)
        {
            throw new ArgumentException("An account cannot have less than $100 at any time.");
        }

        if ((amount / Balance) > 0.9D)
        {
            throw new ArgumentException(
                @"A user cannot withdraw more than 90% of their total balance from an account in a single transaction.");
        }
    }
}