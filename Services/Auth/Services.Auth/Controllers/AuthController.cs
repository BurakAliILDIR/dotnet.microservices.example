using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Auth.Dto;
using Services.Auth.Model;
using Services.Auth.Service;
using Shared.ControllerBase;

namespace Services.Auth.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            var response = await _authService.Register(request);

            return ReturnActionResult(response);
        }


        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto request)
        {
            var response = await _authService.Login(request);

            return ReturnActionResult(response);
        }
    }
}