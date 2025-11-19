namespace BankSystemWebApi.Models.Entities;

public class Customer:BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public int ProfilePictureId { get; set; }
    public CustomerFile ProfilePicture { get; set; }
    
}