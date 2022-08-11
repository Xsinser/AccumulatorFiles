using AccumulatorFiles.Context;
using AccumulatorFiles.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
namespace AccumulatorFiles.Controllers
{
    [ApiController]
    public class FileUploadController : ControllerBase
    {

        [Route("File/Upload")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            return Content(await new FileUploadManager(new SQLiteContext()).SaveFile(file));
        } 
    }
}
