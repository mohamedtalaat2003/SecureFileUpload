# SecureFileUpload

A .NET library for validating uploaded file names and extensions in ASP.NET Core applications.

## Features

- Configurable allowed file extensions.
- File extension validation.
- Rejection of file names containing path separators, colons, or null characters.
- Optional strict policy for rejecting multiple file extensions.

## Installation

```bash
dotnet add package SecureFileUpload
```

## Configuration

Configure the allowed extensions in your application's dependency injection setup:

```csharp
builder.Services.AddSecureFileUpload(options =>
{
    options.AllowedExtensions = new HashSet<string>(
        StringComparer.OrdinalIgnoreCase)
    {
        ".jpg",
        ".png",
        ".pdf"
    };
});
```

## Security Notice

Extension validation alone does not guarantee that a file is safe. Validate file content, generate server-side storage names, and apply appropriate storage and malware-scanning controls where necessary.

## License

MIT
