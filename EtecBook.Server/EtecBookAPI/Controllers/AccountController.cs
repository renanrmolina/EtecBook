using System.Net.Mail;
using EtecBookAPI.Data;
using EtecBookAPI.DataTransferObjetcs;
using EtecBookAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtecBookAPI.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("authenticate")]

        public async Task<IActionResult> Authenticate ([FromBody]LoginDto login)
        {
            if (login == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            // Identificar se o Login é por email ou username
            AppUser user = new();
            if (IsValidEmail(login.Email))
            {
                user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password);
            } else {
                user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == login.Email && u.Password == login.Password);
            }

            if (user == null) {
                return NotFound(new { Message = "Usuário e/ou Senha inválidos!!!"});
            }
                 
            return Ok(new { Message = "Login Realizado"});
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto register)
        {
           if (register == null)
                return BadRequest();
           if (!ModelState.IsValid)
                return BadRequest();
          
          AppUser user = new()
          {
            Id = Guid.NewGuid(),
            Name = register.Name,
            Email = register.Email,
            Password = register.Password,
            Role = "Usuário",
            UserName = register.Email
          };
          
          
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
           
            return Ok();
        }
        private static bool IsValidEmail(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
