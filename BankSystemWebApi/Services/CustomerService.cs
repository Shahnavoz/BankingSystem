using System.Net;
using BankSystemWebApi.Data;
using BankSystemWebApi.Models.DTOs;
using BankSystemWebApi.Models.Entities;
using BankSystemWebApi.Services.Interfaces;
using Dapper;
using UploadUserAvatarWebApi.Responces;

namespace BankSystemWebApi.Services;

public class CustomerService(ApplicationDbContext context):ICustomerService
{
    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        try
        {
            using var conn = context.GetConnection();
            conn.Open();
            string query = @"select * from customers";
            var customers = await conn.QueryAsync<Customer>(query);
            return customers.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<Customer>> GetCustomerByIdAsync(int id)
    {
        try
        {
            using var  conn = context.GetConnection();
            conn.Open();
            string query = @"select * from customers where Id = @Id";
            var customer = await conn.QueryFirstOrDefaultAsync<Customer>(query, new { Id = id });
            return customer==null ? new Response<Customer>(HttpStatusCode.InternalServerError, "An error occured")
                : new Response<Customer>(HttpStatusCode.OK,"Found!", customer);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<Customer>(HttpStatusCode.InternalServerError, "An error occured");
        }
    }

    public async Task<Response<string>> CreateCustomerAsync(CustomerCreateRequestDto customerDto)
    {
        try
        {
            using var conn = context.GetConnection();
            conn.Open();
            var fullPath = await SaveFileAsync(customerDto.ProfilePicture);
            
            var customerFile=new CustomerFile{
                FileName=customerDto.ProfilePicture.FileName,
                FileExtension=Path.GetExtension(customerDto.ProfilePicture.FileName),
                FilePath=fullPath
                };
           const string insertFileQuery = "INSERT INTO customerfiles(FileName,FileExtension,FilePath) VALUES(@FileName,@FileExtension,@FilePath), returning Id";
            
           var fileId = await conn.ExecuteScalarAsync<int>(insertFileQuery, customerFile);
           customerFile.Id = fileId;

           var customer = new Customer
           {
               Name = customerDto.Name,
               Email = customerDto.Email,
               PhoneNumber = customerDto.PhoneNumber,
               ProfilePictureId = customerFile.Id

           };
           
           const string insertCustomerQuery=@"insert into customers(Name,Email,ProfilePictureId) 
                                 values(@Name,@Email,@ProfilePictureId),returning Id";
           
           var customerId = await conn.ExecuteAsync(insertCustomerQuery, customer);
           customer.Id = customerId;
           customer.ProfilePicture = customerFile;
           return customerId==0 ? new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error!")
               : new Response<string>(HttpStatusCode.OK, "Successfully Created Customer!") ;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<string>(HttpStatusCode.InternalServerError, "An error occured");
        }
    }

    public async Task<Response<string>> UpdateCustomerAsync(Customer customer)
    {
        try
        {
            using var conn = context.GetConnection();
            conn.Open();
            string query="update customers set Name = @Name,Email = @Email,ProfilePicture = @ProfilePicture where Id = @Id";
            var customerId = await conn.ExecuteScalarAsync<int>(query, customer);
            return customerId==0 ? new Response<string>(HttpStatusCode.InternalServerError, "An error occured")
                : new Response<string>(HttpStatusCode.OK, "Successfully Updated Customer!") ;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<string>(HttpStatusCode.InternalServerError, "An error occured");
        }
    }

    public async Task<Response<string>> DeleteCustomerAsync(int id)
    {
        try
        {
            using var conn=context.GetConnection();
            conn.Open();
            string query = @"delete from customers where Id = @Id";
            var customer=await conn.ExecuteAsync(query, new { Id = id });
            return customer==0 ? new Response<string>(HttpStatusCode.InternalServerError, "An error occured")
                : new Response<string>(HttpStatusCode.OK, "Successfully Deleted Customer!");
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Response<string>(HttpStatusCode.InternalServerError, "An error occured");
        }
    }
    
    private async Task<string> SaveFileAsync(IFormFile file)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "CustomerFiles",
            $"{DateTime.Now:yy-MM-dd-ss}-{file.FileName}");

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return filePath;
    }
}