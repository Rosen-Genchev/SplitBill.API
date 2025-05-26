namespace SplitBill.API.Entities
{
    public class Expense
    {
        public int Id { get; set; }

        public string Description { get; set; } = null!;
        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int PaidByUserId { get; set; }
        public User PaidBy { get; set; } = null!;

        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;
    }
}
