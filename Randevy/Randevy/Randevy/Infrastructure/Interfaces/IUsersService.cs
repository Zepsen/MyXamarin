using System.Threading.Tasks;
using Randevy.Models;
using Randevy.Models.App;

namespace Randevy.Infrastructure.Interfaces
{
    public interface IUsersService
    {
        Task<HttpResult<Result<UserModel>>> GetUsers(FilterBase filter = null);
        Task<HttpResult<UserModel>> GetUser(string id);
        Task<HttpResult> PostUsers(object requestBody);
        Task<HttpResult> DeleteUsers(string id);
        Task<HttpResult> PutUsers(string id, object requestBody);
        Task<HttpResult> PatchUsers(string id, object requestBody);
    }
}