using Newtonsoft.Json;
using Plugin.Settings.Abstractions;
using Randevy.Infrastructure.Interfaces;
using Randevy.Infrastructure.Interfaces.App;

namespace Randevy.Services.App
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly ISettings _settings;

        public LocalStorageService(ISettings settings)
        {
            _settings = settings;
        }

        #region -- ILocalStorage implementation --

        public T Load<T>(string key)
        {
            var str = _settings.GetValueOrDefault(key, "");
            return JsonConvert.DeserializeObject<T>(str);
        }

        public void Save<T>(string key, T val)
        {
            _settings.AddOrUpdateValue(key, JsonConvert.SerializeObject(val));
        }

        public void InvalidateAllAsync()
        {
            _settings.Clear();
        }

        #endregion
    }
}
