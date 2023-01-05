namespace Application.Project.Dtos
{
    public class CreateProjectRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public int EstimatedWorkingHours { get; set; }
        public double BillRate { get; set; }

        public CreateProjectRequestDto()
        {
        }

        public CreateProjectRequestDto(string name, string description, string clientName, int estimatedWorkingHours, double billRate)
        {
            Name = name;
            Description = description;
            ClientName = clientName;
            EstimatedWorkingHours = estimatedWorkingHours;
            BillRate = billRate;
        }
    }
}