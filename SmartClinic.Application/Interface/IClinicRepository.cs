// SmartClinic.Application/Interfaces/IClinicRepository.cs

using System.Threading.Tasks;

namespace SmartClinic.Application.Interfaces
{
    public interface IClinicRepository
    {
        Task<bool> ClinicExists(Guid clinicId);
    }
}