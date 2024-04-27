namespace Domain.Abstraction
{
    public interface IFileData
    {
        string FileName { get; }
        string ContentType { get; }
        long Length { get; }
        Stream OpenReadStream();
    }
}
