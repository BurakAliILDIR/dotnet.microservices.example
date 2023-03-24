using Services.Auth.Dto;
using Services.Auth.Model;
using Shared.Util;

namespace Services.Auth.Service
{
    public interface IAuthService
    {
        Task<Response> Register(UserRegisterDto request);
    }
}
