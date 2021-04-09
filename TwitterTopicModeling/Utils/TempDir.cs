//https://gist.github.com/JoeHartzell/ab6ebd4af690c79e84c728f5da367dcc
//This is the location where this code was taken from

using System;
using System.IO;

namespace TwitterTopicModeling.Utils
{
     class TempDir : IDisposable
    {
        /// <summary>
        /// The path to the directory
        /// </summary>
        /// <value></value>
        public string Name { get; init; }

        private TempDir() { }

        /// <summary>
        /// Creates a new temporary directory
        /// </summary>
        /// <returns></returns>
        public static TempDir Create()
        {
            var uuid = Guid.NewGuid();
            var path = Path.Combine(Path.GetTempPath(), uuid.ToString());
            var dir = Directory.CreateDirectory(path);

            return new TempDir
            {
                Name = dir.FullName
            };

        }

        public void Dispose()
        {
            Directory.Delete(Name, true);
        }
    }


}