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
        private readonly IConfiguration _configuration;


        public AuthService(IOptions<AppSettings> appSettings, AppDbContext dbContext, IMapper mapper,
            IConfiguration configuration)
        {
            _appSettings = appSettings.Value;
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<Response> Register(UserRegisterDto request)
        {
            var exists = await _dbContext.Users.Where(x => x.Username == request.Username).AnyAsync();

            if (exists)
                return Response.Return(Response.ResponseStatusEnum.Warning, "Kullanıcı adı kullanılıyor.", null, 400);

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            request.Password = passwordHash;

            var user = _mapper.Map<User>(request);

            await _dbContext.Users.AddAsync(user);

            await _dbContext.SaveChangesAsync();

            return Response.Return(Response.ResponseStatusEnum.Success, "Başarıyla kayıt oldunuz.", user);
        }

        public async Task<Response> Login(UserLoginDto request)
        {
            var user = await _dbContext.Users.Where(x => x.Username == request.Username).FirstOrDefaultAsync();

            if (user is null)
            {
                return Response.Return(Response.ResponseStatusEnum.Error, "Kullanıcı adı veya şifre yanlış.", null,
                    400);
            }

            bool exists = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

            if (!exists)
            {
                return Response.Return(Response.ResponseStatusEnum.Error, "Kullanıcı adı veya şifre yanlış.", null,
                    400);
            }


            var jwtToken = CreateToken(user);

            return Response.Return(Response.ResponseStatusEnum.Success, "Başarıyla giriş yaptınız.", jwtToken, 200);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:JwtToken")
                    .Value!)); // appsettings.json da tanımlandı.

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}