namespace Application.Project.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public int TeamId { get; set; } = 0;
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

        public ProjectDto(int id, int teamId, string name, string description, string clientName,
            int clientId, DateTime startDate, DateTime estimatedDeliveryDate,
            int estimatedWorkingHours, double hoursInvested,
            double billRate, bool isActive)
        {
            Id = id;
            TeamId = teamId;
            Name = name;
            Description = description;
            ClientName = clientName;
            ClientId = clientId;
            StartDate = startDate;
            EstimatedDeliveryDate = estimatedDeliveryDate;
            EstimatedWorkingHours = estimatedWorkingHours;
            HoursInvested = hoursInvested;
            BillRate = billRate;
            IsActive = isActive;
        }

        public ProjectDto()
        {
        }
    }
}