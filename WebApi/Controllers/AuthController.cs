using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ResultWrapper.Library;
using WebApi.Brokers;
using WebApi.Common;
using WebApi.Contracts;
using WebApi.Contracts.Notification;
using WebApi.Exceptions;
using WebApi.Extensions;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : AuthorizedControllerBase
{
     private readonly AppDbContext _context;
     private readonly IWebHostEnvironment _environment;
     private readonly NotificationBroker _notificationBroker;

     public AuthController(AppDbContext context, IWebHostEnvironment environment, NotificationBroker notificationBroker)
     {
          _context = context;
          _environment = environment;
          _notificationBroker = notificationBroker;
     }

     [HttpPost("sign"), AllowAnonymous]
     public async Task<Wrapper> Sign([FromBody] SignUserDto dto)
     {
          var phoneNumber = dto.PhoneNumber.Replace("-", "");

          var user = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == dto.PhoneNumber);

          var otp =/* _environment.IsProduction() ? PasswordHelper.GenerateRandom6DigitNumber().ToString() : */"777777";
          var verificationKey = Guid.NewGuid();

          await using var transaction = await _context.Database.BeginTransactionAsync();
          try
          {
               if (user is null)
               {
                    if (dto.Name.IsNullOrEmpty())
                         throw new BadRequestException("Name field is required");

                    user = new User()
                    {
                         PhoneNumber = phoneNumber,
                         Name = dto.Name!,
                         VerificationCode = otp,
                         VerificationKey = verificationKey,
                         Role = Roles.Client
                    };

                    user = _context.Users.Add(user).Entity;
               }
               else
               {
                    user.VerificationCode = otp;
                    user.VerificationKey = verificationKey;

                    _context.Users.Update(user);
               }

               await _context.SaveChangesAsync();

               await _notificationBroker.SendSmsAsync(new SendMessageDto()
               {
                    PhoneNumber = user.PhoneNumber.Replace("+",""),
                    From = "4546",
                    Message = $"Логин: {user.PhoneNumber} \nПароль: {otp}",
                    CallbackUrl = null
               });

               await transaction.CommitAsync();
          }
          catch (Exception e)
          {
               await transaction.RollbackAsync();
               throw;
          }

          return (verificationKey.ToString(), 200);
     }

     [HttpPost("verify"), AllowAnonymous]
     public async Task<Wrapper> Verify([FromBody] VerifyDto dto)
     {
          var user = await _context.Users.FirstOrDefaultAsync(x =>
               x.VerificationKey == dto.Key && x.VerificationCode == dto.Code);

          if (user is null)
               throw new NotFoundException("OTP code is invalid");

          JwtSecurityTokenHandler tokenHandler = new();
          byte[] key = Encoding.UTF8.GetBytes(JwtRequirements.SigningKey);
          SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
          {
               Subject = new ClaimsIdentity(new[]
               {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Name, user.Name)
               }),
               Expires = DateTime.UtcNow.AddMinutes(60),
               SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
          };

          SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
          string jwtToken = tokenHandler.WriteToken(token);

          this.Response.Cookies.Append(JwtRequirements.TokenNameInCookie,
               jwtToken,
               new CookieOptions()
               {
                    Expires = DateTimeOffset.Now.AddMinutes(60),
                    Secure = true,
                    HttpOnly = true
               });

          return 200;
     }

     [HttpPost("sign-out"), AllowAnonymous]
     public new Wrapper SignOut()
     {
          this.Response.Cookies.Delete(JwtRequirements.TokenNameInCookie);
          return 200;
     }

     [HttpGet("users")]
     [Authorize(Roles.Admin)]
     public async Task<Wrapper> GetAllUsers([FromQuery] string? phoneNumber, [FromQuery] string? name)
     {
          var q = _context.Users.Select(x => new
          {
               x.Id,
               x.Name,
               x.PhoneNumber
          });
          return (await q.ToListAsync(), await q.CountAsync());
     }

     [HttpGet("me")]
     [Authorize(Roles.Client)]
     public async Task<Wrapper> GetMe()
     {
          var me = await _context.Users.Select(x => new
          {
               x.Id,
               x.Name,
               x.PhoneNumber,
               x.Role
          }).FirstOrDefaultAsync(x => x.Id == this.UserId);
          return (me, 200);
     }
}