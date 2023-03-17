using JavidElectronics.Contracts.File;
using JavidElectronics.Services.Abstracts;

namespace JavidElectronics.Services.Concretes
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService>? _logger;

        public FileService(ILogger<FileService>? logger)
        {
            _logger = logger;
        }

        public async Task<string> UploadAsync(IFormFile formFile, UploadDirectory uploadDirectory)
        {
            string directoryPath = GetUploadDirectory(uploadDirectory);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var imageNameInSystem = GenerateUniqueFileName(formFile.FileName);
            var filePath = Path.Combine(directoryPath, imageNameInSystem);

            try
            {
                using FileStream fileStream = new FileStream(filePath, FileMode.Create);
                await formFile.CopyToAsync(fileStream);
            }
            catch (Exception e)
            {
                _logger!.LogError(e, "Error occured in file service");

                throw;
            }

            return imageNameInSystem;
        }

        public async Task DeleteAsync(string? fileName, UploadDirectory uploadDirectory)
        {
            var deletePath = Path.Combine(GetUploadDirectory(uploadDirectory), fileName);

            await Task.Run(() => File.Delete(deletePath));
        }

        private string GetUploadDirectory(UploadDirectory uploadDirectory)
        {
            string startPath = Path.Combine("wwwroot", "client", "custom-files");

            switch (uploadDirectory)
            {
                case UploadDirectory.Slider:
                    return Path.Combine(startPath, "sliders");
                case UploadDirectory.Product:
                    return Path.Combine(startPath, "products");
                case UploadDirectory.PaymentBenefit:
                    return Path.Combine(startPath, "paymentbenefits");
                case UploadDirectory.BlogImage:
                    return Path.Combine(startPath, "BlogImages");
                case UploadDirectory.BlogVideo:
                    return Path.Combine(startPath, "BlogVideos");
                default:
                    throw new Exception("Something went wrong");
            }
        }

        private string GenerateUniqueFileName(string fileName)
        {
            return $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        }

        public string GetFileUrl(string? fileName, UploadDirectory uploadDirectory)
        {
            string initialSegment = "client/custom-files/";

            switch (uploadDirectory)
            {
                case UploadDirectory.Slider:
                    return $"{initialSegment}/sliders/{fileName}";
                case UploadDirectory.Product:
                    return $"{initialSegment}/products/{fileName}";
                case UploadDirectory.PaymentBenefit:
                    return $"{initialSegment}/paymentbenefits/{fileName}";
                case UploadDirectory.BlogImage:
                    return $"{initialSegment}/BlogImages/{fileName}";
                case UploadDirectory.BlogVideo:
                    return $"{initialSegment}/BlogVideos/{fileName}";
                default:
                    throw new Exception("Something went wrong");
            }
        }
    }
}
