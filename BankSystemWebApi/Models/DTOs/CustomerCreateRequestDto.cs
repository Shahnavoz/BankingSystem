namespace BankSystemWebApi.Models.DTOs;

public class CustomerCreateRequestDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public IFormFile ProfilePicture { get; set; }
}