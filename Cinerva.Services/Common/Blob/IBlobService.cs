using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinerva.Services.Common.Blob
{
    public interface IBlobService
    {
        Task<IEnumerable<string>> AllBlobs(string containerName);
        Task<bool> DeleteBlob(string name, string containerName);
        Task<bool> UploadBlob(string name, IFormFile file, string containerName);
    }
}
