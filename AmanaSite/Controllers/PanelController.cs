using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace AmanaSite.Controllers
{
    public class PanelController :Controller
    {
         public ActionResult Index(){
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","panel","index.html"),"text/HTML");
        }
    }
}