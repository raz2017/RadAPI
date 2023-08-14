using RadancyAPI.Domain;

namespace RadancyAPI.Controllers.Inputs;

public struct UpdateAccountInput
{
    public Guid UserId { get; set; }
    public Guid AccountId { get; set; }
    public AccountAction AccountAction { get; set; }
    public double Amount { get; set; }
}