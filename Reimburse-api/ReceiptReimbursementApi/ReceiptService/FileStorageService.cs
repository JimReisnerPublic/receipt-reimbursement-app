//using System.ComponentModel.DataAnnotations;
//using System.Linq;

//namespace ReceiptReimbursement.Services
//{
//    // IFileStorageService.cs
//    public interface IFileStorageService
//    {
//        Task<string> SaveReceiptFileAsync(IFormFile file);
//        void DeleteReceiptFile(string filePath);
//    }

//    // FileStorageService.cs
//    public class FileStorageService : IFileStorageService
//    {
//        private readonly string _uploadsPath;

//        public FileStorageService(IWebHostEnvironment env)
//        {
//            _uploadsPath = Path.Combine(env.ContentRootPath, "Uploads");
//            Directory.CreateDirectory(_uploadsPath);
//        }

//        public async Task<string> SaveReceiptFileAsync(IFormFile file)
//        {
//            var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png" };
//            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

//            if (!allowedExtensions.Contains(extension))
//                throw new ValidationException("Invalid file type. Allowed types: PDF, JPG, PNG");

//            if (file.Length > 5 * 1024 * 1024) // 5MB limit
//                throw new ValidationException("File size exceeds 5MB limit");

//            var safeFileName = $"{Guid.NewGuid()}{extension}";
//            var filePath = Path.Combine(_uploadsPath, safeFileName);

//            using (var stream = new FileStream(filePath, FileMode.Create))
//            {
//                await file.CopyToAsync(stream);
//            }

//            return $"Uploads/{safeFileName}";
//        }

//        public void DeleteReceiptFile(string filePath)
//        {
//            if (File.Exists(filePath))
//                File.Delete(filePath);
//        }
//    }

//}
