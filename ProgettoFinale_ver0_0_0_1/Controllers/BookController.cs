using AppFinaleLibri.Models;
using esDef.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProgettoFinale_ver0_0_0_1.Managers.Interfaces;
using ProgettoFinale_ver0_0_0_1.Repositories.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AppFinaleLibri.Controllers
{

    [ApiController]
    [Route("[controller]")]  
    public class BookController : ControllerBase
    {
        private readonly IOrderManager _orderManager;
        private readonly IBookManager _bookManager;

        public BookController(IOrderManager orderManager, IBookManager bookManager)
        {
            _orderManager = orderManager;
            _bookManager = bookManager;
        }

        [Authorize]
        [HttpGet("GetBookList")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> GetBookList()
        {
            try
            {
                var sol = await _bookManager.GetBookList();
                return Ok(sol);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }



        [Authorize]
        [HttpPost("PostBooksFiltered")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> PostBooksFiltered([Required][FromBody] SimpleBook a)
        {
            try
            {
                var sol = await _bookManager.GetBookListFiltered(a);
                return Ok(sol);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }



        [Authorize]
        [HttpPost("PostCreateBook")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Book))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> PostCreateBook([FromBody] SimpleBook p)
        {
            try
            {
                await _bookManager.CreateBook(p);
                return Ok("the book has been created");
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }




        [Authorize]
        [HttpPost("PostOrder")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(Book))]
        [SwaggerResponse(StatusCodes.Status404NotFound, null, null)]
        public async Task<IActionResult> PostOrder([Required][FromBody] SimpleBook o)
        {
            try
            {
                Guid Id = Guid.Parse(HttpContext.User.Identity.Name);
                 await _orderManager.CreateOrder(o, Id);
                return Ok("Ordine riuscito");
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
