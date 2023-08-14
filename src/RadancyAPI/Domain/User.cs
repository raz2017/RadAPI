namespace RadancyAPI.Domain;

public record User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DOB { get; set; }
    public User(string firstName, string lastName, string email, DateTime dob, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        DOB = dob;
    }
}