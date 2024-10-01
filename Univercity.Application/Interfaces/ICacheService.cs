namespace University.Application.Interfaces
{
    public interface ICacheService
    {
        T GetData<T>(string key);
        bool SetData<T>(string key, T value, DateTimeOffset expirationDate);
        object RemoveData(string key);
    }
}
