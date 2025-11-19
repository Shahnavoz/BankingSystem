using BankSystemWebApi.Models.Entities;
using BankSystemWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using UploadUserAvatarWebApi.Responces;

namespace BankSystemWebApi.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class CustomerFileController(ICustomerFileService fileService):ControllerBase
{
    [HttpGet("GetCustomerFiles")]
    public async Task<List<CustomerFile>> GetCustomerFiles()
    {
        return await fileService.GetCustomerFilesAsync();
    }

    [HttpGet]
    public async Task<Response<CustomerFile>> GetCustomerFile(int id)
    {
        return await fileService.GetCustomerFileByIdAsync(id);
    }

    [HttpPost]
    public async Task<Response<string>> CreateCustomerFileAsync(IFormFile file)
    {
        return await fileService.CreateCustomerFileAsync(file);
    }
    
}