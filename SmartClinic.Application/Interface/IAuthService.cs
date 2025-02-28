// SmartClinic.Application/Interfaces/IAuthService.cs
using SmartClinic.Domain.DTOs;

namespace SmartClinic.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto model);
        Task<string> LoginAsync(LoginDto model);
    }
}