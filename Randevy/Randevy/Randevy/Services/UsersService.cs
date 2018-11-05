using System.Threading.Tasks;
using Randevy.Infrastructure.Interfaces;
using Randevy.Infrastructure.Interfaces.App;
using Randevy.Models;
using Randevy.Models.App;

namespace Randevy.Services
{
    public class UsersService : IUsersService
    {
        private readonly IHttpService _httpService;
        private readonly string _ctrl;

        public UsersService(IHttpService httpService)
        {
            _httpService = httpService;
            _ctrl = "api/users";
        }

        public async Task<HttpResult<Result<UserModel>>> GetUsers(FilterBase filter = null)
        {
            return await _httpService.GetAsync<Result<UserModel>>(_ctrl, filter);
        }

        public async Task<HttpResult<UserModel>> GetUser(string id)
        {
            return await _httpService.GetAsync<UserModel>($"{_ctrl}/{id}", null);
        }

        public async Task<HttpResult> PostUsers(object requestBody)
        {
            return await _httpService.PostAsync(_ctrl, requestBody, true);
        }

        public async Task<HttpResult> DeleteUsers(string id)
        {
            return await _httpService.DeleteAsync($"{_ctrl}/{id}", true);
        }

        public async Task<HttpResult> PutUsers(string id, object requestBody)
        {
            return await _httpService.PutAsync($"{_ctrl}/{id}", requestBody, true);
        }

        public async Task<HttpResult> PatchUsers(string id, object requestBody)
        {
            return await _httpService.PatchAsync($"{_ctrl}/{id}", requestBody, true);
        }
    }
}
