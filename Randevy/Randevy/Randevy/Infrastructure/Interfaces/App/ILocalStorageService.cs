namespace Randevy.Infrastructure.Interfaces.App
{
    public interface ILocalStorageService
    {
        T Load<T>(string key);

        void Save<T>(string key, T val);

        void InvalidateAllAsync();
    }
}