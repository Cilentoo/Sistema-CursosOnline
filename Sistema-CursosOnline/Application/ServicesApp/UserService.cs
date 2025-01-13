using Microsoft.IdentityModel.Tokens;
using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Sistema_CursosOnline.Domain.IRepository;
using Sistema_CursosOnline.Domain.IServices;
using System.CodeDom.Compiler;

namespace Sistema_CursosOnline.Application.ServicesApp
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _jwtsecret;
        private readonly string _jwtExpirationMinutes;

        public UserService(IUserRepository userRepository, string jwtsecret, string jwtExpirationMinutes)
        {
            _userRepository = userRepository;
            _jwtsecret = jwtsecret;
            _jwtExpirationMinutes = jwtExpirationMinutes;
        }

        public async Task<string> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.AuthenticateAsync(email, password);
            if (user == null) 
            {
                throw new UnauthorizedAccessException("Email ou senha inválidos");
            }
            return GenerateJwtToken(user);
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString(),
                Status = user.Status,
            };
        }

        public async Task InactivateUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new InvalidOperationException("Usuário não encontrado");
            }

            user.Status = "Inativo";
            await _userRepository.InactiveAsync(id);
        }

        public async Task<UserDTO> RegisterAsync(UserDTO userDTO, string password)
        {
            var user = new User
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                Role = Enum.Parse<EType>(userDTO.Role, ignoreCase: true),
                Status = "Ativo"
            };

            await _userRepository.AddAsync(user, password);

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString(),
                Status = user.Status
            };
        }

        public async Task UpdateUserAsync(int id, UserDTO userDTO)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if(user == null)
            {
                throw new InvalidOperationException("Usuário não encontrado");
            }

            user.Name = userDTO.Name;
            user.Email = userDTO.Email;
            user.Status = userDTO.Status;

            await _userRepository.UpdateAsync(user);
        }


        private string GenerateJwtToken (User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsecret);
            if(!int.TryParse(_jwtExpirationMinutes, out var expirationMinutes))
            {
                throw new InvalidOperationException("JWT ExpirationMinutes is not a valid integer");
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
