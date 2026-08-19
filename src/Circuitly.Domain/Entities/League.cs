namespace Circuitly.Domain.Entities;

public class League
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Format { get; set; }
    
    public DateTime CreatedAt { get; set; }
}