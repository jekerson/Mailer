using Domain.Abstraction;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Files
{
    public class FileData : IFileData
    {
        private readonly IFormFile _formFile;

        public FileData(IFormFile formFile)
        {
            _formFile = formFile;
        }

        public string FileName => _formFile.FileName;

        public string ContentType => _formFile.ContentType;

        public long Length => _formFile.Length;

        public Stream OpenReadStream()
        {
            return _formFile.OpenReadStream();
        }
    }
}
