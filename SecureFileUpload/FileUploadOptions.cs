using System;
using System.Collections.Generic;
using System.Text;

namespace SecureFileUpload
{
    public sealed class FileUploadOptions
    {
        public HashSet<string> AllowedExtensions { get; set; }
    = new(StringComparer.OrdinalIgnoreCase);
    }
}
