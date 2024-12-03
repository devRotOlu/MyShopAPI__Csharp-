using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace MyShopAPI.Services.Image
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
