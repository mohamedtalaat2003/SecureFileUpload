using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using SecureFileUpload;

namespace SecureFileUploadTests
{
    public class FileUploadValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void FileNameWithAllowedExtensions_IsNullOrEmpty_returnFalse(string fileName)
        {
            //arraneg
            var options = Options.Create(new FileUploadOptions
            {
                AllowedExtensions = new HashSet<string> { ".jpg", ".png", ".gif" }
            });


            var validator = new FileExtensionValidator(options);

            //act
            var actual = validator.IsValid(fileName);

            //assert
            Assert.False(actual);

        }

        //public static bool IsValid(string fileName)
        //{
        //    if (string.IsNullOrEmpty(fileName))
        //        return false;

        //    // Reject path-like or malformed names.
        //    if (fileName.Contains('/') ||
        //        fileName.Contains('\\') ||
        //        fileName.Contains(':') ||
        //        fileName.Contains('\0'))
        //    {
        //        return false;
        //    }

        //    var extension = Path.GetExtension(fileName);

        //    // The final extension must be explicitly allowed.
        //    if (!_options.AllowedExtensions.Contains(extension))
        //        return false;

        //    // Reject multiple extensions, e.g. photo.jpg.php.
        //    var nameWithoutFinalExtension =
        //        Path.GetFileNameWithoutExtension(fileName);

        //    if (Path.HasExtension(nameWithoutFinalExtension))
        //        return false;

        //    return true;
        //}
    }
}
