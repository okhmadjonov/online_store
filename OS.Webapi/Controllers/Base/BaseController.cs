using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using OS.Application.interfaces;

namespace OS.Webapi.Controllers.Base
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        public const string LocalHeaderKey = "local";

        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

        internal Guid UserId
        {
            get
            {
                if (User?.Identity?.IsAuthenticated != true) return Guid.Empty;
                var claimValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Guid.TryParse(claimValue, out var userId) ? userId : Guid.Empty;
            }
        }

        protected async Task<string> GetLocal(IOSDbContext _context)
        {
            string local = string.Empty;
            var defaultLang = await _context.Languages.FirstOrDefaultAsync(x => x.IsDefault);

            if (Request.Headers.TryGetValue(BaseController.LocalHeaderKey, out StringValues langCode))
            {
                var lang = await _context.Languages.FirstOrDefaultAsync(x => x.Code == langCode.ToString().ToUpper());
                if (lang == null)
                {
                    local = defaultLang?.Code ?? "UZ";
                }
                else
                {
                    local = lang.Code;
                }

            }
            else
            {
                local = defaultLang?.Code ?? "UZ";
            }
            return local;
        }
    }
}
