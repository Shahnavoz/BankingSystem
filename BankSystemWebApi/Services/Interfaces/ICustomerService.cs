using BankSystemWebApi.Models.DTOs;
using BankSystemWebApi.Models.Entities;
using UploadUserAvatarWebApi.Responces;

namespace BankSystemWebApi.Services.Interfaces;

public interface ICustomerService
{
    Task<List<Customer>> GetAllCustomersAsync();
    Task<Response<Customer>> GetCustomerByIdAsync(int id);
    Task<Response<string>> CreateCustomerAsync(CustomerCreateRequestDto customerDto);
    Task<Response<string>> UpdateCustomerAsync(Customer customer);
    Task<Response<string>> DeleteCustomerAsync(int id);
    
}