using BankSystemWebApi.Models.Entities;
using UploadUserAvatarWebApi.Responces;

namespace BankSystemWebApi.Services.Interfaces;

public interface ICustomerFileService
{
    Task<List<CustomerFile>> GetCustomerFilesAsync();
    Task<Response<CustomerFile>> GetCustomerFileByIdAsync(int id);
    Task<Response<string>> CreateCustomerFileAsync(IFormFile file);
    Task<Response<CustomerFile>> UpdateCustomerFileAsync(CustomerFile fileName);
    Task<Response<CustomerFile>> DeleteCustomerFileAsync(int id);
}