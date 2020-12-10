using System;
using System.Abstractions;
using System.IO.Abstractions;
using System.Linq;

namespace MyLib
{
    /// <summary>
    /// Demo class for playing with the System.IO.Abstractions package
    /// </summary>
    public class FolderTidy
    {
        /// <summary>
        /// Delete all files older than a given time period, with respect
        /// to the current UtcNow time and the file's CreateTimeUtc.
        /// </summary>
        /// <param name="folder">Folder to process</param>
        /// <param name="oldestAgeToKeep">Maximum age of files to keep</param>
        /// <param name="fileSystem">Provides file system services</param>
        /// <param name="dateTimeServices">Provides date and time services</param>
        public static void DeleteOldFiles(
            string folder,
            TimeSpan oldestAgeToKeep,
            IFileSystem fileSystem,
            IDateTimeServices dateTimeServices)
        {
            var now = dateTimeServices.UtcNow;

            var filesToDelete =
                fileSystem
                .DirectoryInfo
                .FromDirectoryName(folder)
                .GetFiles()
                .Where(fi => (now - fi.CreationTimeUtc) > oldestAgeToKeep);

            foreach(var fileInfo in filesToDelete)
            {
                fileInfo.Delete();
            }
        }
    }
}
