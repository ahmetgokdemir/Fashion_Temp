using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.IdentityServer.DTOs;
using Project.IdentityServer.Models;
using System.Threading.Tasks;

namespace Project.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly UserManager<ApplicationUser> user /*{ get; set; }*/;

        //[HttpPost]
        //public async Task<IActionResult> SignUp(SignUpDto signUpDto)
        //{


        //}




    }
}
