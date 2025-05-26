namespace SplitBill.API.Entities
{
    public class GroupMember
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;

    }
}
