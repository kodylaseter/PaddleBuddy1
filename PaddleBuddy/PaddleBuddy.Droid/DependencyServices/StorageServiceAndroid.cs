using System;
using System.IO;
using System.Linq;
using PaddleBuddy.Core.DependencyServices;

namespace PaddleBuddy.Droid.DependencyServices
{
    public class StorageServiceAndroid : BaseDependencyServiceAndroid, IStorageService
    {
        public void SaveSerializedToFile(string json, string name)
        {
            var path = GetPath(name);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (var file = File.Open(path, FileMode.Create, FileAccess.Write))
            {
                using (var stream = new StreamWriter(file))
                {
                    stream.Write(json);
                }
            }
        }

        public string ReadSerializedFromFile(string name)
        {
            return File.ReadAllText(GetPath(name));
        }

        public string GetPath(string name)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), name + ".txt");
        }

        public bool HasData(string[] names)
        {
            return names.All(a => File.Exists(GetPath(a)));
        }
    }
}