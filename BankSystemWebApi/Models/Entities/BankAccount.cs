namespace BankSystemWebApi.Models.Entities;

public class BankAccount:BaseEntity
{
    public string AccountNumber { get; set; }
    public string AccountType { get; set; }
    public decimal Balance { get; set; }
    public int CustomerId { get; set; }
    
}