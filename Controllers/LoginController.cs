using System.Threading.Tasks;
using Blog.Data;
using Blog.Entities;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Blog.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        [HttpPost("")]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromBody] User model,
            [FromServices] AppDataContext context)
        {
            // Recupera o usuário
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user == null)
                return NotFound();

            if (!PasswordHasher.Verify(user.PasswordHash, model.PasswordHash))
                return BadRequest(new { Message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(user);

            // Retorna os dados
            return new
            {
                user = user.Email,
                token = token
            };
        }
    }
}
