using AccumulatorFiles.Context;
using AccumulatorFiles.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AccumulatorFiles.Managers
{
    public class FileInteractionManager
    {
        private IContext context = null;
        public FileInteractionManager(IContext context)
        {
            this.context = context;
        }
        public List<FileModel> GetFilesList()
        {
            var result = context.File.ToList();
            return result;
        }
        public void DeleteFile(int fileId)
        {
            var fileObject = context.File.FirstOrDefault(fod => fod.Id == fileId);
            if (fileObject == null)
                throw new System.Exception("406 Not Acceptable");
            if (!File.Exists(fileObject.Directory + fileObject.Name))
                throw new System.Exception("404 Not Found");
            File.Delete(fileObject.Directory + fileObject.Name);
            context.File.Remove(fileObject);
            (context as DbContext).SaveChanges();

        }
        public async Task<PhysicalFileModel> GetFile(FormatModel format, int fileId)
        {

            var fileManager = new FileManager();
            var fileObject = context.File.FirstOrDefault(fod => fod.Id == fileId);
            if (fileObject == null)
                throw new System.Exception("406 Not Acceptable");
            if (!File.Exists(fileObject.Directory + fileObject.Name))
                throw new System.Exception("404 Not Found");
            var file = await fileManager.GetFileByLineByFilter(fileObject.Directory + fileObject.Name, format);
            var sortedFileManager = new SortedFileManager();
            sortedFileManager.Order(format, ref file);
            var filePath = await fileManager.CreateTempFileFromObject(file);
            PhysicalFileModel physicalFileModel = new PhysicalFileModel() { Name = fileObject.Name, Path = filePath, Type = "application/csv" };
            return physicalFileModel;
        }
    }
}
