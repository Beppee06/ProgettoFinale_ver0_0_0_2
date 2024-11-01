﻿using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using esDef.Models;
using ProgettoFinale_ver0_0_0_1.Managers.Interfaces;

namespace esDef.Controllers
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
        public async Task<IActionResult> LoginAsync([FromBody] SimpleUser u)
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
        public async Task<IActionResult> RegisterAsync([FromBody] SimpleUser u)
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
