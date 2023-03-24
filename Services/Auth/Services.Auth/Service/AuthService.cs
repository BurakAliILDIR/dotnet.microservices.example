using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using Services.Auth.Config;
using Services.Auth.Model;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services.Auth.Dto;
using Shared.Util;
using AutoMapper;

namespace Services.Auth.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;


        public AuthService(IOptions<AppSettings> appSettings, AppDbContext dbContext, IMapper mapper)
        {
            _appSettings = appSettings.Value;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response> Register(UserRegisterDto request)
        {
            var exists = await _dbContext.Users.Where(x => x.Username == request.Username).AnyAsync();

            if (exists)
                return Response.Return(Response.ResponseStatusEnum.Warning, "Kullanıcı adı kullanılıyor.", null, 403);


            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            request.Password = passwordHash;

            var user = _mapper.Map<User>(request);

            await _dbContext.Users.AddAsync(user);

            return Response.Return(Response.ResponseStatusEnum.Success, "Başarıyla kayıt oldunuz.", user);
        }
    }
}