using System.Net;
using BankSystemWebApi.Data;
using BankSystemWebApi.Models.Entities;
using BankSystemWebApi.Services.Interfaces;
using Dapper;
using UploadUserAvatarWebApi.Responces;

namespace BankSystemWebApi.Services;

public class AccountService(ApplicationDbContext context):IAccountService
{
    public async Task<List<BankAccount>> GetAllBankAccountsAsync()
    {
        try
        {
            using var conn = context.GetConnection();
            conn.Open();
            string query = "select * from accounts";
            var accounts = await conn.QueryAsync<BankAccount>(query);
            return accounts.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<BankAccount>> GetBankAccountByIdAsync(int id)
    {
        try
        {
            using var conn = context.GetConnection();
            conn.Open();
            string query = "select * from accounts where id = @id";
            var account = await conn.QueryFirstOrDefaultAsync<BankAccount>(query, new { id });
            return account == null ? new Response<BankAccount>(HttpStatusCode.InternalServerError,"Internal Server Error!")
                : new Response<BankAccount>(HttpStatusCode.OK,"Found successfully!", account);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<BankAccount>(HttpStatusCode.InternalServerError, "Internal Server Error!");
        }
    }

    public async Task<Response<string>> CreateBankAccountAsync(BankAccount bankAccount)
    {
        try
        {
            using var conn = context.GetConnection();
            conn.Open();
          const  string query=@"insert into accounts (accountnumber,accounttype,balance,customerid) 
                           values (@accountnumber,@accounttype,@balance,@customerid)";
          var result = await conn.ExecuteAsync(query, bankAccount);
          return result==0 ? new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error!")
              : new Response<string>(HttpStatusCode.OK, "Account created successfully!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error!");
        }
    }

    public async Task<Response<string>> UpdateBankAccountAsync(BankAccount bankAccount)
    {
        try
        {
            using var conn = context.GetConnection();
            conn.Open();
            string query="update accounts set accounttype=@accounttype,balance=@balance,customerid=@customerid";
            var result = await conn.ExecuteAsync(query, bankAccount);
            return result==0 ? new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error!")
                : new Response<string>(HttpStatusCode.OK, "Account updated successfully!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error!");
        }
    }

    public async Task<Response<string>> DeleteBankAccountAsync(int id)
    {
        try
        {
            using var conn = context.GetConnection();
            conn.Open();
            string query = "delete from accounts where id = @id";
            var result = await conn.ExecuteAsync(query, new { id });
            return result==0 ? new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error!")
                : new Response<string>(HttpStatusCode.OK, "Account deleted successfully!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error!");
        }
    }
}