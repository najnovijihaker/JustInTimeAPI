namespace Domain.Entities
{
    public class TimeKeeping
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int ProjectId { get; set; }
        public int AccountId { get; set; }
        public double HoursWorked { get; set; }
    }
}