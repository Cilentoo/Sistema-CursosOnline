using Sistema_CursosOnline.Application.DTO;

namespace Sistema_CursosOnline.Domain.IServices
{
    public interface IUserService
    {
        Task<string> AuthenticateAsync(string email, string password);
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<UserDTO> RegisterAsync(UserDTO userDTO, string password);
        Task UpdateUserAsync(int id, UserDTO userDTO);

        Task InactivateUserAsync(int id);
    }
}
