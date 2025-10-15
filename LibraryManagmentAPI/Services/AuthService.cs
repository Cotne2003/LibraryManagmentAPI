using LibraryManagmentAPI.Data;
using LibraryManagmentAPI.Models;
using LibraryManagmentAPI.Models.Entities;
using LibraryManagmentAPI.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LibraryManagmentAPI.Services
{
    public class AuthService : IAuthRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<TokenResponseDto> Login(LogAuthorDto logAuthorDto)
        {
            var author = await _context.Authors
                .FirstOrDefaultAsync(a => a.Name.ToLower() == logAuthorDto.Name.ToLower());

            if (author is null)
                throw new Exception("User not found");

            if (new PasswordHasher<Author>().VerifyHashedPassword(author, author.PasswordHash, logAuthorDto.Password)
                == PasswordVerificationResult.Failed)
            {
                new Exception("Password is incorrect");
            }

            var response = new TokenResponseDto
            {
                AccessToken = CreateToken(author),
                RefreshToken = await GenerateAndSaveRefreshToken(author)
            };

            return response;
        }

        public async Task<Author> Register(AddAuthorDto addAuthorDto)
        {
            if (await _context.Authors.AnyAsync(a => a.Name == addAuthorDto.Name))
                throw new Exception("User already exists");

            var author = new Author();

            var hashedPassword = new PasswordHasher<Author>()
                .HashPassword(author, addAuthorDto.Password);

            author.Name = addAuthorDto.Name;
            author.PasswordHash = hashedPassword;

            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            return author;
        }

        public async Task<TokenResponseDto> RefreshTokens(RefreshTokenRequestDto refreshTokenRequestDto)
        {
                var author = await ValidateRefreshToken(refreshTokenRequestDto.AuthorId, refreshTokenRequestDto.RefreshToken);

                var response = new TokenResponseDto
                {
                    AccessToken = CreateToken(author),
                    RefreshToken = await GenerateAndSaveRefreshToken(author)
                };

                return response;
        }

        private async Task<Author> ValidateRefreshToken(Guid authorId, string refreshToken)
        {
            var author = await _context.Authors.FindAsync(authorId);
            if (author is null || author.RefreshToken != refreshToken || author.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new Exception("Invalid author id or refresh token");

            return author;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveRefreshToken(Author author)
        {
            var refreshToken = GenerateRefreshToken();
            author.RefreshToken = refreshToken;
            author.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _context.SaveChangesAsync();
            return refreshToken;
        }

        private string CreateToken(Author author)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, author.Id.ToString()),
                new Claim(ClaimTypes.Name, author.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
