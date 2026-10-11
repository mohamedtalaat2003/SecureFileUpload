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

        [Theory]
        [InlineData("/")]
        [InlineData("\\")]
        [InlineData(":")]
        [InlineData("\0")]
        public void FileNameWithUnAllowedPaths_ReturnFalse(string fileName)
        {
            //arrange
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

        [Theory]
        [InlineData("photo.jpg.php")]
        [InlineData("document.pdf.exe")]
        public void FileNameWithMultipleExtensions_ReturnFalse(string fileName)
        {
            //arrange
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


        [Theory]
        [InlineData("photo.php")]
        [InlineData("document.exe")]
        [InlineData("image.bmp")]
        public void FileNameWithUnAllowedExtensions_ReturnFalse(string fileName)
        {
            //arrange
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


        [Theory]
        [InlineData("../photo.Php")]
        [InlineData(@"folder\photo.Exe")]
        [InlineData("photo.asp:.CPS")]
        public void FileNameWithUnAllowedExtensionsWithDifferentCase_ReturnFalse(string fileName)
        {
            //arrange
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

        [Theory]
        [InlineData("photo.Png")]
        [InlineData("photo.gIf")]
        [InlineData("photo.jpg")]
        [InlineData("photo.Jpg")]
        public void FileNameWithAllowedExtensionsWithDifferentCase_ReturnTrue(string fileName)
        {
            //arrange
            var options = Options.Create(new FileUploadOptions
            {
                AllowedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".jpg", ".png", ".gif" }
            });
            var validator = new FileExtensionValidator(options);
            //act
            var actual = validator.IsValid(fileName);
            //assert
            Assert.True(actual);
        }

        [Theory]
        [InlineData(".png.jpg")]
        [InlineData(".jpg.gif")]
        [InlineData(".Jpg.gif")]
        public void FileNameWithAllowedExtensionsWithMultipleExtensions_ReturnFalse(string fileName)
        {
            //arrange
            var options = Options.Create(new FileUploadOptions
            {
                AllowedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".jpg", ".png", ".gif" }
            });
            var validator = new FileExtensionValidator(options);
            //act
            var actual = validator.IsValid(fileName);
            //assert
            Assert.False(actual);
        }

        [Theory]
        [InlineData("photo.php.exe")]
        [InlineData("document.eXe.doc")]
        public void FileNameWithUnAllowedExtensionsWithMultipleExtensions_ReturnFalse(string fileName)
        {
            //arrange
            var options = Options.Create(new FileUploadOptions
            {
                AllowedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".jpg", ".png", ".gif" }
            });
            var validator = new FileExtensionValidator(options);
            //act
            var actual = validator.IsValid(fileName);
            //assert
            Assert.False(actual);

        }

        [Theory]
        [InlineData("photo")]
        [InlineData("document")]
        public void FilenameWithoutExtension_ReturnFalse(string fileName)
        {
            //arrange
           var options = Options.Create(new FileUploadOptions
           {
               AllowedExtensions = new HashSet<string>{ ".jpg", ".png", ".gif" }
           });

            var validator = new FileExtensionValidator(options);

            //act
            var actual = validator.IsValid(fileName);
            //assert
            Assert.False(actual);
        }

        [Theory]
        [InlineData("photo.jpg")]
        [InlineData("document.png")]
        [InlineData("image.gif")]
        public void FilenameWithAllowedExtension_IsValidFunctionRemovedExtension_ReturnTrue(string fileName)
        {
            //arrange
            var options = Options.Create(new FileUploadOptions
            {
                AllowedExtensions = new HashSet<string> { ".jpg", ".png", ".gif" }
            });

            var validator = new FileExtensionValidator(options);

            //act
            var actual = validator.IsValid(fileName);
            //assert
            Assert.True(actual);
        }
    }
}


    

