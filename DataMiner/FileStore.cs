using System;

namespace DataMiner
{
    public class FileStore<T> where T : class
    {
        public FileStore()
        {
            LastSaved = DateTime.Now;
        }

        public DateTime LastSaved { get; set; }
        public TimeSpan Expires { get; set; }
        public T Data { get; set; }
    }
}