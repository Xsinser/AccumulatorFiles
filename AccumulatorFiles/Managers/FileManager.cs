using AccumulatorFiles.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

//!!!!!!!
#nullable enable
//!!!!!!!
namespace AccumulatorFiles.Managers
{
    public class FileManager : IDisposable
    {
        private string? _tempFile = null;

        public async Task<SortedFileModelByColumns> GetFileByLine(string filePath)
        {
            SortedFileModelByColumns result = new SortedFileModelByColumns();
            List<string[]> bufferList = new List<string[]>();
            using (StreamReader reader = new StreamReader(path: filePath))
            {
                string? line = null;
                line = await reader.ReadLineAsync();
                if (string.IsNullOrEmpty(line))
                {
                    result.Th = new string[0];
                    result.Columns = new string[0][];
                    result.Columns[0] = result.Th;
                    return result;
                }
                result.Th = line.Split(',');
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    bufferList.Add(line.Split(','));
                }
            }
            result.Columns = bufferList.ToArray();
            return result;
        }

        public async Task<SortedFileModelByColumns> GetFileByLineByFilter(string filePath, FormatModel format)
        {
            SortedFileModelByColumns result = new SortedFileModelByColumns();
            List<string[]> bufferList = new List<string[]>();
            using (StreamReader reader = new StreamReader(path: filePath))
            {
                string? line = null;
                line = await reader.ReadLineAsync();
                if (string.IsNullOrEmpty(line))
                {
                    result.Th = new string[0];
                    result.Columns = new string[0][];
                    result.Columns[0] = result.Th;
                    return result;
                }
                result.Th = line.Split(',');
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    string[] strings = line.Split(',');
                    bool isCorrect = true;
                    foreach (KeyValuePair<int, Regex> element in format.Filters)
                    {
                        if (!element.Value.IsMatch(strings[element.Key]))
                        {
                            isCorrect = false;
                            break;
                        }
                    }
                    if (isCorrect)
                        bufferList.Add(strings);
                }
            }
            result.Columns = bufferList.ToArray();
            return result;
        }
        public async Task<string> CreateTempFileFromObject(SortedFileModelByColumns file)
        {
            string filePath = Startup.Configuration["tempFilesDirectory"] + $"TEMPFILE_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}_{Guid.NewGuid()}";
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                await sw.WriteLineAsync(file.Th.GetString(','));
                for (int i = 0; i < file.Columns.Length; i++)
                {
                    await sw.WriteLineAsync(file.Columns[i].GetString(','));
                }
            }
            this._tempFile = filePath;
            return filePath;
        }

        #region Подчищаем файлы
        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                if (!string.IsNullOrEmpty(this._tempFile))
                {
                    if (File.Exists(this._tempFile))
                        File.Delete(this._tempFile);
                }
            }
            disposed = true;
        }
        ~FileManager()
        {
            Dispose(false);
        }
        #endregion
        //public SortedFileModelByRows GetRows(this SortedFileModelByColumns file)
        //{
        //    SortedFileModelByRows rows = new SortedFileModelByRows();
        //    for(int i = 0; i < file.Columns.Length; i++)
        //    {
        //        for(int j = 0;j<file.Th)
        //    }
        //    return rows;
        //}


        #region static
        public async static Task<string> GetTHAsync(string filePath)
        {
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                return await streamReader.ReadLineAsync();
            }
        }
        public static string GetTH(string filePath)
        {
            return GetTHAsync(filePath).Result;
        }
        public static string[] GetNamesColumns(string filePath)
        {
            return GetTHAsync(filePath).Result.Split(',');
        }
        public async static Task<string[]> GetNamesColumnsAsync(string filePath)
        {
            return (await GetTHAsync(filePath)).Split(',');
        }
        #endregion
    }
}
