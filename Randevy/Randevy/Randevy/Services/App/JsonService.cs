using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Randevy.Infrastructure.Constants;
using Randevy.Infrastructure.Interfaces;
using Randevy.Infrastructure.Interfaces.App;

namespace Randevy.Services.App
{
    public class JsonService : IJsonService
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();

        #region -- IJsonService implementation --

        public string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value, converters: new JsonConverter[] { new IsoDateTimeConverter() });
        }

        public StringContent SerializeContent(object value)
        {
            return new StringContent(SerializeObject(value), Encoding.UTF8, Constants.MediaType.AppJson);
        }

        public T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, converters: new JsonConverter[] { new IsoDateTimeConverter() });
        }

        public T DeserializeStream<T>(JsonTextReader jsonTextReader)
        {
            _serializer.Converters.Add(new IsoDateTimeConverter());
            return _serializer.Deserialize<T>(jsonTextReader);
        }

        #endregion
    }
}
