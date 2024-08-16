using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Exceptions;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
public class AuthorizedControllerBase : ControllerBase
{
     private long _userId = 0;

     public long UserId
     {
          get
          {
               if (_userId > 0)
                    return _userId;

               var nameIdentifier = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
               if (long.TryParse(nameIdentifier, out long userId))
               {
                    _userId = userId;
                    return userId;
               }

               throw new NotFoundException("User not found");
          }
     }
}