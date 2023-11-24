using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace APIBashExcecution.Controllers
{
    public class ScriptRequest
    {
        public string ScriptName { get; set; }
        public string Arguments { get; set; }
    }

    public class PagingRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class ScriptController : Controller
    {
        [HttpGet("test-query")]
        public ActionResult Index(int testParam, PagingRequest pagingRequest)
        {
            return Ok( new 
            {
                page = pagingRequest.PageNumber,
                total = pagingRequest.PageSize,
                testParam
            });
        }

        [HttpPost("run")]
        public IActionResult RunScript([FromBody] ScriptRequest request)
        {
            string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts", request.ScriptName);

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "powershell", // Use "pwsh" for PowerShell Core, use "powershell" for Windows PowerShell
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                Arguments = $"-File \"{scriptPath}\" {request.Arguments}"
            };

            using (Process process = new Process { StartInfo = psi })
            {
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    return Ok(new { Output = output });
                }
                else
                {
                    return BadRequest(new { Error = error });
                }
            }
        }
    }
}
