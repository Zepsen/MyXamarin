using System.Net.Http;
using Newtonsoft.Json;

namespace Randevy.Infrastructure.Interfaces.App
{
    public interface IJsonService
    {
        string SerializeObject(object requestBody);
        StringContent SerializeContent(object value);

        T DeserializeStream<T>(JsonTextReader json);
        
    }
}