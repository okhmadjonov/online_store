using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OS.Application.Common.Models;
using OS.Application.Operations.Auth.Commands.Login;
using OS.Application.Operations.Auth.Commands.RefreshToken;
using OS.Application.Operations.Auth.Commands.Register;
using OS.Application.Operations.Auth.Dtos;
using OS.Webapi.Controllers.Base;

namespace OS.Webapi.Controllers
{
    [ApiController]
    public class AuthController : BaseController
    {
        /// <summary>
        /// Registers a new user with optional role assignment (User, Administrator, SuperAdministrator)
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Register([FromBody] RegisterUserCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(ApiResponse<AuthResponseDto>.Success(response, "Foydalanuvchi muvaffaqiyatli ro'yxatdan o'tdi."));
        }

        /// <summary>
        /// Authenticates user and returns JWT access token & refresh token
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login([FromBody] LoginUserCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(ApiResponse<AuthResponseDto>.Success(response, "Muvaffaqiyatli tizimga kirildi."));
        }

        /// <summary>
        /// Refreshes expired JWT access token using a valid refresh token
        /// </summary>
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(ApiResponse<AuthResponseDto>.Success(response, "Token muvaffaqiyatli yangilandi."));
        }
    }
}
