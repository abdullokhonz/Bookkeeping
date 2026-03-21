using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.ImageDto;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Infrastructure.Data;
using Bookkeeping.Infrastructure.Repositories;
using Bookkeeping.Services.Implementations.Base;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;

namespace Bookkeeping.Services.Implementations.CashReceiptOrders
{
    public class ImageService : BaseService<Image>, IImageService
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _uploadFolder = Path.Combine("uploads", "images");

        public ImageService(
            IWebHostEnvironment env,
            IPostgreSQLRepository<Image> repository,
            IConfiguration config,
            PostgreSQLDbContext context,
            ILogger<ImageService> logger)
            : base(repository, config, context, logger)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public async Task<Result<Image>> UploadImageAsync(ImageCreateDto dto, IFormFile file, CancellationToken ct)
        {
            if (file == null || file.Length == 0)
                return Result<Image>.Failure(DomainErrors.Image.EmptyFile);

            string savedPath = await SaveFileToDiskAsync(file);

            var entity = new Image
            {
                Name = dto.Name,
                Description = dto.Description,
                EntityId = dto.EntityId,
                Path = savedPath,
            };

            return await base.CreateAsync(entity, ct);
        }

        public async Task<Result> UpdateImageAsync(Guid id, ImageUpdateDto dto, IFormFile? file, CancellationToken ct)
        {
            var existingResult = await base.GetByIdAsync(id, ct);
            if (existingResult.IsFailure)
                return Result.Failure(existingResult.Error);

            var existing = existingResult.Value;

            if (file != null)
            {
                await DeleteFileFromDiskAsync(existing.Path);
                existing.Path = await SaveFileToDiskAsync(file);
            }

            if (!string.IsNullOrEmpty(dto.Name)) existing.Name = dto.Name;
            if (!string.IsNullOrEmpty(dto.Description)) existing.Description = dto.Description;
            if (dto.EntityId.HasValue) existing.EntityId = dto.EntityId.Value;

            existing.UpdatedAt = DateTime.UtcNow;

            return await base.UpdateAsync(id, existing, ct);
        }

        public override async Task<Result> DeleteAsync(Guid id, CancellationToken ct)
        {
            var entityResult = await base.GetByIdAsync(id, ct);
            if (entityResult.IsFailure)
                return Result.Failure(entityResult.Error);

            await DeleteFileFromDiskAsync(entityResult.Value.Path);

            return await base.DeleteAsync(id, ct);
        }

        private async Task<string> SaveFileToDiskAsync(IFormFile file)
        {
            string webRootPath = _env.WebRootPath
                ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            string uploadsDir = Path.Combine(webRootPath, _uploadFolder);
            if (!Directory.Exists(uploadsDir))
                Directory.CreateDirectory(uploadsDir);

            string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            string fullPath = Path.Combine(uploadsDir, uniqueFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var result = Path.Combine(_uploadFolder, uniqueFileName).Replace("\\", "/");

            return result;
        }

        private async Task DeleteFileFromDiskAsync(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) return;

            try
            {
                string webRootPath = _env.WebRootPath
                    ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                string fullPath = Path.Combine(
                    webRootPath,
                    relativePath.TrimStart('/')
                                .Replace("/", Path.DirectorySeparatorChar.ToString())
                );

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Не удалось удалить файл: " + ex.Message);
            }
        }
    }
}
