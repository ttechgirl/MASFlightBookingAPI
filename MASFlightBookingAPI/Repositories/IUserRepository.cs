using MASFlightBookingAPI.View_Models;
using System.Threading.Tasks;

namespace MASFlightBookingAPI.Repositories
{
    public interface IUserRepository
    {
        Task CreateUser(RegisterModel registerModel);
        Task LoginUser(LoginModel loginModel);

    }
}
