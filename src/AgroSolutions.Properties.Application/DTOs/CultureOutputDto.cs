namespace AgroSolutions.Properties.Application.DTOs
{
    public record CultureOutputDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public int MaxTemperature { get; set; }
        public int MinTemperature { get; set; }
        public int MaxMoist { get; set; }
        public int MinMoist { get; set; }
        public int MaxPrecipitation { get; set; }
        public int MinPrecipitation { get; set; }
        public DateTime CreatedAt { get; init; }
    }
}
