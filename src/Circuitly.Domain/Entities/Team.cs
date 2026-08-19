namespace Circuitly.Domain.Entities;

public class Team
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public Guid LeagueId { get; set; }
    
    public League? League { get; set; }
}