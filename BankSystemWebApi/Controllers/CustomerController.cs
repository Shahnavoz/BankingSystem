using BankSystemWebApi.Models.DTOs;
using BankSystemWebApi.Models.Entities;
using BankSystemWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using UploadUserAvatarWebApi.Responces;

namespace BankSystemWebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomerController(ICustomerService customerService):ControllerBase
{
    [HttpPost("CreateCustomer")]
    public async Task<Response<string>> CreateCustomerAsync(CustomerCreateRequestDto customer)
    {
        return await customerService.CreateCustomerAsync(customer);
    }

    [HttpGet("GetAllCustomers")]
    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await customerService.GetAllCustomersAsync();
    }

    [HttpGet]
    public async Task<Response<Customer>> GetCustomerByIdAsync(int id)
    {
        return await customerService.GetCustomerByIdAsync(id);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateCustomerAsync(Customer customer)
    {
        return await customerService.UpdateCustomerAsync(customer);
    }
    [HttpDelete]
    public async  Task<Response<string>> DeleteCustomerAsync(int id)
    {
        return await customerService.DeleteCustomerAsync(id);
    }
}