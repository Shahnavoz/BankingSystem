using BankSystemWebApi.Models;
using Npgsql;

namespace BankSystemWebApi.Data;

public class ApplicationDbContext(ApplicationConfig config)
{
    public NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(config.DbConnectionString);
    }
    
}