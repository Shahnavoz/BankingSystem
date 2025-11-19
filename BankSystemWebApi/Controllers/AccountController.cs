using BankSystemWebApi.Models.Entities;
using BankSystemWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using UploadUserAvatarWebApi.Responces;

namespace BankSystemWebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController(IAccountService accountService):ControllerBase
{
    [HttpGet("GetAccounts")]
    public async Task<List<BankAccount>> GetAccountsAsync()
    {
        return await accountService.GetAllBankAccountsAsync();
    }

    [HttpGet("{id}")]
    public async Task<Response<BankAccount>> GetAccountAsync(int id)
    {
        return await accountService.GetBankAccountByIdAsync(id);
    }

    [HttpPost]
    public async Task<Response<string>> CreateAccountAsync(BankAccount account)
    {
        return await accountService.CreateBankAccountAsync(account);
    }

    [HttpPut("{id}")]
    public async Task<Response<string>> UpdateAccountAsync(BankAccount account)
    {
        return await accountService.UpdateBankAccountAsync(account);
    }

    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteAccountAsync(int id)
    {
        return await accountService.DeleteBankAccountAsync(id);
    }
    
}