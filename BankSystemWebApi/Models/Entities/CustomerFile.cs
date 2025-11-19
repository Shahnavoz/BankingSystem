namespace BankSystemWebApi.Models.Entities;

public class CustomerFile:BaseEntity
{
    
    public string FileName { get; set; }
    public string FileExtension { get; set; }
    public string FilePath { get; set; }
    
    public CustomerFile(){}

    public CustomerFile(string fileName, string fileExtension, string filePath)
    {
        FileName = fileName;
        FileExtension = fileExtension;
        FilePath = filePath;
    }
    
}