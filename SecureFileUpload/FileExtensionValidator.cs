using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureFileUpload
{
    internal sealed class FileExtensionValidator(IOptions<FileUploadOptions> options)
    {
        private static readonly FileUploadOptions _options = options.Value;

        public static bool IsValid(string fileName , HashSet<string> allowedExtensions)
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
