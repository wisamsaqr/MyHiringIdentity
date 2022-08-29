using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hiring.Data.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Hiring.Service.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHostingEnvironment hostingEnvironment;
        public FileService(IHostingEnvironment hostingEnvironment, IWebHostEnvironment webHostEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> SaveFile(IFormFile file, string webRootInnerPath)
        {
            string uniqueFileName = null;
            if (file != null && file.Length > 0)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, webRootInnerPath);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                //FileStream fileStream = new FileStream(filePath, FileMode.Create);
                //await using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                await using FileStream fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);
            }

            return uniqueFileName;
        }

        public async Task<string> SaveFile2(IFormFile file, string webRootInnerPath)
        {
            string uniqueFileName = null;
            if (file != null && file.Length > 0)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, webRootInnerPath);
                uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                await using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            return uniqueFileName;
        }
    }

    public interface IFileService
    {
        public Task<string> SaveFile(IFormFile file, string webRootInnerPath);
        public Task<string> SaveFile2(IFormFile file, string webRootInnerPath);
    }
}

/*
Classic sync using
Classic using calls the Dispose() method of an object implementing the IDisposable interface.

using var disposable = new Disposable();
// Do Something...

Is equivalent to

IDisposable disposable = new Disposable();
try
{
    // Do Something...
}
finally
{
    disposable.Dispose();
}
New async await using
The new await using calls and await the DisposeAsync() method of an object implementing the IAsyncDisposable interface.

await using var disposable = new AsyncDisposable();
// Do Something...

Is equivalent to

IAsyncDisposable disposable = new AsyncDisposable();
try
{
    // Do Something...
}
finally
{
    await disposable.DisposeAsync();
}
The IAsyncDisposable Interface was added in .NET Core 3.0 and .NET Standard 2.1.

In .NET, classes that own unmanaged resources usually implement the IDisposable interface to provide a mechanism for releasing unmanaged resources synchronously. However, in some cases they need to provide an asynchronous mechanism for releasing unmanaged resources in addition to (or instead of) the synchronous one. Providing such a mechanism enables the consumer to perform resource-intensive dispose operations without blocking the main thread of a GUI application for a long time.

The IAsyncDisposable.DisposeAsync method of this interface returns a ValueTask that represents the asynchronous dispose operation. Classes that own unmanaged resources implement this method, and the consumer of these classes calls this method on an object when it is no longer needed.
*/