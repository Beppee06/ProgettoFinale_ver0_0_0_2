using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using esDef.Models;
using Microsoft.AspNetCore.Authorization;
using ProgettoFinale_ver0_0_0_1.Repositories.Interfaces;


namespace AppFinaleLibri.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderManager _orderManager;
        public OrderController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }




        [Authorize]
        [HttpGet("GetOrders")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> GetOrders()
        {
            Guid Id = Guid.Parse(HttpContext.User.Identity.Name);
            try
            {
                var sol = await _orderManager.GetOrders(Id);
                return Ok(sol);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
