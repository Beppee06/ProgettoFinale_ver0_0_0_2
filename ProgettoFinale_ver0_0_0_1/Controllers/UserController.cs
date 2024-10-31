using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ProgettoFinale_ver0_0_0_1.Models.Users;
using ProgettoFinale_ver0_0_0_1.Managers.Interfaces.Users;
using System.ComponentModel.DataAnnotations;

namespace ProgettoFinale_ver0_0_0_1.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    { 
        private readonly IUserManager _userManager;
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }



        [HttpPost("Login")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> LoginAsync([Required][FromBody] SimpleUser u)
        {
            try
            {
                var token = await _userManager.Login(u);
                return Ok(new { token = token });
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }




        [HttpPost("Register")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> RegisterAsync([Required][FromBody] SimpleUser u)
        {
            try
            {
                string token = await _userManager.Register(u);
                return Ok(new { token = token });
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
