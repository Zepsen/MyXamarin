using System.Collections.Generic;
using System.Threading.Tasks;
using Randevy.Models.App;

namespace Randevy.Infrastructure.Interfaces.App
{
    public interface IHttpService
    {
        Task<HttpResult<T>> GetAsync<T>(string resource, FilterBase filter, bool withAuth = true, Dictionary<string, string> additioalHeaders = null);
                
        Task<HttpResult> PostAsync(string resource, object requestBody, bool withAuth = true, Dictionary<string, string> additioalHeaders = null);

        Task<HttpResult<T>> PostAsync<T>(string resource, object requestBody, bool withAuth = true, Dictionary<string, string> additioalHeaders = null);

        Task<HttpResult> PutAsync(string resource, object requestBody, bool withAuth = true, Dictionary<string, string> additioalHeaders = null);

        Task<HttpResult> DeleteAsync(string resource, bool withAuth = true, Dictionary<string, string> additioalHeaders = null);

        Task<HttpResult> PatchAsync(string resource, object requestBody, bool withAuth = true, Dictionary<string, string> additioalHeaders = null);

        
    }
}
        