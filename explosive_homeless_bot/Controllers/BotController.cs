using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace explosive_homeless_bot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BotController : Controller
    {
        [HttpGet]
        public string Get([FromBody] string msg)
        {
            Console.WriteLine("hovno");
            return "Received "+msg;
        }
    }
}
