using Sistema_CursosOnline.Application.DTO;

namespace Sistema_CursosOnline.Domain.IServices
{
    public interface IUserService
    {
        Task<string> AuthenticateAsync(string email, string password);
        Task<UserDTO> GetUserByIdAsync(int id);
    }
}
