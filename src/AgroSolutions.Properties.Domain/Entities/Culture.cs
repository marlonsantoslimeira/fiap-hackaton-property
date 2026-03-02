namespace AgroSolutions.Properties.Domain.Entities;

public class Culture 
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;

    public int MaxTemperature { get; set; }
    public int MinTemperature { get; set; }

    public int MaxMoist { get; set; }
    public int MinMoist { get; set; }

    public int MaxPrecipitation { get; set; }
    public int MinPrecipitation { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}