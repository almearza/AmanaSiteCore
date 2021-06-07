using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Text;
namespace AmanaSite.Controllers.Api
{
    public class TempController : ApiBaseController
    {
        private readonly IWebHostEnvironment _env;
        public TempController(IWebHostEnvironment env)
        {
            this._env = env;

        }
        [HttpGet]
        public ActionResult GetUsers()
        {
            var path = Path.Combine(_env.ContentRootPath,"Data",@"weatherdataseed.json");
            var dataText = System.IO.File.ReadAllText(path);
            var users =System.Text.Json.JsonSerializer.Deserialize<ApiResponse>(dataText);
            return Ok(users);
        }
    }
    public class User
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class ApiResponse
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<User> data { get; set; }
    }
}