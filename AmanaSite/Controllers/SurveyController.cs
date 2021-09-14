using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AmanaSite.Controllers
{
   
    public class SurveyController : Controller
    {
        public IActionResult Index(string lang)
        {
            return View();
        }

      
    }
}