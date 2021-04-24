using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    public class SecretController : Controller
    {
        [Authorize]
        [Route("/secret")]
        public string Index()
        {
            var claims=User.Claims.ToString();
            return claims;
        }
    }
}
