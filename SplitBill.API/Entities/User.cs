
using SplitBill.API.Entities; 


public class User
{
    public int Id { get; set; }
    public string Username { get; set; }

    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Role { get; set; } = "User"; // Default role
    public ICollection<GroupMember> GroupMemberships { get; set; }

}
