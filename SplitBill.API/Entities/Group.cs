namespace SplitBill.API.Entities
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Навигационни свойства
        public ICollection<GroupMember> Members { get; set; } = new List<GroupMember>();
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
