using System;
using System.Collections.Generic;
using System.Text;

namespace SecureFileUpload
{
    public interface IFileExtensionValidator
    {
        public bool IsValid(string fileName);
    }
}
