namespace RadancyAPI.Controllers.Inputs;

public struct UserInput
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DOB { get; set; }
    public string Email { get; set; }
}