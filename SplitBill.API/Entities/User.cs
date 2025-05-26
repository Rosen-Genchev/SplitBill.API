namespace SplitBill.API.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        // Навигационни свойства
        public ICollection<GroupMember> GroupMemberships { get; set; } = new List<GroupMember>();
        public ICollection<Expense> ExpensesPaid { get; set; } = new List<Expense>();
    }
}
