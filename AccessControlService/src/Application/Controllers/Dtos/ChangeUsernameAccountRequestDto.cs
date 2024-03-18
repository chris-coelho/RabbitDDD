namespace Application.Controllers.Dtos;

public class ChangeUsernameAccountRequestDto
{
    public Guid accountId { get; set; }
    public string username { get; set; }
}