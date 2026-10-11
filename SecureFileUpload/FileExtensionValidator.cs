using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureFileUpload
{
    public sealed class FileExtensionValidator : IFileExtensionValidator
    {
        private readonly FileUploadOptions _options;

        public FileExtensionValidator(IOptions<FileUploadOptions> options)
        {
            _options = (options ?? throw new ArgumentNullException(nameof(options))).Value;
        }   

        public bool IsValid(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;

            // Reject path-like or malformed names.
            if (fileName.Contains('/') ||
                fileName.Contains('\\') ||
                fileName.Contains(':') ||
                fileName.Contains('\0'))
            {
                return false;
            }

            var extension = Path.GetExtension(fileName);

            // The final extension must be explicitly allowed.
            if (!_options.AllowedExtensions.Contains(extension))
                return false;

            // Reject multiple extensions, e.g. photo.jpg.php.
            var nameWithoutFinalExtension =
                Path.GetFileNameWithoutExtension(fileName);

            if (Path.HasExtension(nameWithoutFinalExtension))
                return false;

            return true;
        }
    }
}
