using BankSystemWebApi.Models.Entities;
using UploadUserAvatarWebApi.Responces;

namespace BankSystemWebApi.Services.Interfaces;

public interface IAccountService
{
    Task<List<BankAccount>> GetAllBankAccountsAsync();
    Task<Response<BankAccount>> GetBankAccountByIdAsync(int id);
    Task<Response<string>> CreateBankAccountAsync(BankAccount bankAccount);
    Task<Response<string>> UpdateBankAccountAsync(BankAccount bankAccount);
    Task<Response<string>> DeleteBankAccountAsync(int id);
}