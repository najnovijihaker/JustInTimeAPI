namespace Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        //public string RepositoryLink { get; set; } = string.Empty; mozda kasnije
        public string ClientName { get; set; } = string.Empty;

        public int ClientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public int EstimatedWorkingHours { get; set; }
        public double HoursInvested { get; set; }
        public double BillRate { get; set; }
        public bool IsActive { get; set; }
    }
}