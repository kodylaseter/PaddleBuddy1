namespace PaddleBuddy.Core.DependencyServices
{
    public interface IStorageService
    {
        void SaveSerializedToFile(string json, string name);
        string ReadSerializedFromFile(string name);
        string GetPath(string name);
        bool HasData(string[] names);
    }
}
