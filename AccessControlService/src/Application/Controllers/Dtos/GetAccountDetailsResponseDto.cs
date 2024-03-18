namespace Application.Controllers.Dtos;

// ReSharper disable InconsistentNaming
public class GetAccountDetailsResponseDto
{
    public Guid id { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public bool active { get; set; }
    public DateTime createdOn { get; set; }
}