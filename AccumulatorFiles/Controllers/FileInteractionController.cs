using AccumulatorFiles.Context;
using AccumulatorFiles.Managers;
using AccumulatorFiles.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AccumulatorFiles.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FileInteractionController : Controller
    {
        [Route("FileInteraction/Test")]
        [Authorize]
        public async Task<IActionResult> Test()
        {
            //var tt = new FileManager();
            
            //var result = await tt.GetFileByLine(@"D:\AccumulatorFiles\AccumulatorFiles\AccumulatorFiles\Files\ss.csv");

            return Json(User.Identity.Name);
        }

        [Route("FileInteraction/Delete")]
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete()
        {
            var bodyStr = "";
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = await reader.ReadToEndAsync();
            }
            JToken token = JObject.Parse(bodyStr); 
            var fileId = token.SelectToken("FileId").ToObject<int>();
            FileInteractionManager manager = new FileInteractionManager(new SQLiteContext());
            manager.DeleteFile(fileId);
            return Content("Done");
        }

        [Route("FileInteraction/GetFilesList")]
        [Authorize]
        public IActionResult GetFilesList()
        {
            var fileManager = new FileInteractionManager(new SQLiteContext());

            return Json(fileManager.GetFilesList());
        }

 

        [Route("File/GetFile")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GeFile()
        {
            var bodyStr = "";
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = await reader.ReadToEndAsync();
            } 
            JToken token = JObject.Parse(bodyStr);
            var ColumnSortingSequence = token.SelectToken("ColumnSortingSequence");
            var filters = token.SelectToken("Filters");
            var Descending = token.SelectToken("Descending");  
            var model = new FormatModel(filters, ColumnSortingSequence, Descending);
            var fileId = token.SelectToken("FileId").ToObject<int>();
            FileInteractionManager manager = new FileInteractionManager(new SQLiteContext());

            PhysicalFileModel result = await manager.GetFile(model, fileId);
            return PhysicalFile(result.Path, result.Type, result.Name);
        }

 
    }
}
