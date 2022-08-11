using AccumulatorFiles.Context;
using AccumulatorFiles.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AccumulatorFiles.Managers
{
    public class FileUploadManager
    {
        private IContext context = null;
        public FileUploadManager(IContext context)
        {
            this.context = context;
        }

        public async Task<string> SaveFile(IFormFile file)
        {
            if (file != null)
            {
                string path = Startup.Configuration["filesDirectory"] + file.FileName;
                if (await context.File.FirstOrDefaultAsync(foda => foda.Name == file.Name) == null)
                {

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    var fileModel = new FileModel() { Name = file.FileName, Directory = Startup.Configuration["filesDirectory"], Columns = await FileManager.GetTHAsync(path) };
                    await context.File.AddAsync(fileModel);
                    (context as DbContext).SaveChanges();
                    return "201 Created";
                }
                else
                    throw new System.Exception("409 Conflict");

            } 
            else
            {
                return "204 No Content";
            }
        }

    }
}
