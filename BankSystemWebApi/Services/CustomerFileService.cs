using System.Net;
using BankSystemWebApi.Data;
using BankSystemWebApi.Models.Entities;
using BankSystemWebApi.Services.Interfaces;
using Dapper;
using UploadUserAvatarWebApi.Responces;

namespace BankSystemWebApi.Services;

public class CustomerFileService(ApplicationDbContext context):ICustomerFileService
{
    public async Task<List<CustomerFile>> GetCustomerFilesAsync()
    {
        try
        {
            using var conn = context.GetConnection();
            conn.Open();
            string query = "SELECT * FROM customerfiles";
            var files = await conn.QueryAsync<CustomerFile>(query);
            return files.ToList();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<CustomerFile>> GetCustomerFileByIdAsync(int id)
    {
        try
        {
            using var conn = context.GetConnection();
            conn.Open();
            string query = "SELECT * FROM customerfiles WHERE id = @id";
            var customerFile = await conn.QueryFirstOrDefaultAsync<CustomerFile>(query, new { id });
            return customerFile==null ? new Response<CustomerFile>(HttpStatusCode.InternalServerError,"Internal Server Error!")
                : new Response<CustomerFile>(HttpStatusCode.OK,"Found successfuly!", customerFile);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<CustomerFile>(HttpStatusCode.InternalServerError,"Internal Server Error!");
        }
    }

    public async  Task<Response<string>> CreateCustomerFileAsync(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
                return new Response<string>(HttpStatusCode.BadRequest, "File is empty");
            if (file.Length > 5 * 1024 * 1024) 
                return new Response<string>(HttpStatusCode.BadRequest, "File is too large");
            
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "CustomerFiles");
            
            var uniqueFileName = Guid.NewGuid()+Path.GetExtension(file.FileName);
            var path = Path.Combine(uploadsFolder, uniqueFileName);
            
            await using var stream=new FileStream(path, FileMode.CreateNew);
            await file.CopyToAsync(stream);
            using var conn = context.GetConnection();
            conn.Open();
            var dbFile = new CustomerFile
            {
                FileName = file.FileName,
                FileExtension = Path.GetExtension(file.FileName),
                FilePath = $"/Uploads/CustomerFiles/{uniqueFileName}"
            };
            const string insertFileQuery = @"INSERT INTO customerfiles(FileName,FileExtension,FilePath) VALUES(@FileName,@FileExtension,@FilePath)
                                          returning id";
            var fileId = await conn.ExecuteScalarAsync<int>(insertFileQuery, dbFile);
            return fileId==0 ? new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error!")
                : new Response<string>(HttpStatusCode.OK,"Created successfuly!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<Response<CustomerFile>> UpdateCustomerFileAsync(CustomerFile fileName)
    {
        throw new NotImplementedException();
    }

    public Task<Response<CustomerFile>> DeleteCustomerFileAsync(int id)
    {
        throw new NotImplementedException();
    }
}