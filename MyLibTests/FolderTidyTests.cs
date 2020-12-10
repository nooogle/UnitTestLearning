using System;
using NSubstitute;
using Xunit;
using FluentAssertions;
using System.Linq;
using System.IO.Abstractions.TestingHelpers;

namespace MyLibTests
{
    /// <summary>
    /// Unit tests for the folder tidy demo class.
    /// </summary>
    /// <remarks>
    /// Using NSubstitute to provide a mocked date-time and a
    /// virtualised file system.
    /// Using FluentAssertions for enhanced assertion checking.
    /// </remarks>
    public class FolderTidyTests
    {
        [Fact]
        public void Basic()
        {
            // Prep the file system
            var folder = @"c:\dummy";
            MockFileSystem fileSystem = CreateVirtualFileSystemWithFolder(folder);
            DateTime startTime = Create10FilesEach1DayApart(folder, fileSystem);


            // Setup the virtual date-time system so that 'now' is 10 days
            // after the first file was created
            var dateTimeServices = Substitute.For<System.Abstractions.IDateTimeServices>();
            dateTimeServices.UtcNow.Returns(startTime + TimeSpan.FromDays(10));


            // Run the folder tidy, drop everything older than 5 days ago
            MyLib.FolderTidy.DeleteOldFiles(
                folder: folder,
                oldestAgeToKeep: TimeSpan.FromDays(5),
                fileSystem: fileSystem,
                dateTimeServices: dateTimeServices);


            // And check it worked...!
            fileSystem.Directory.GetFiles(folder).Length.Should().Be(5);
        }



        private static DateTime Create10FilesEach1DayApart(
            string folder,
            MockFileSystem fileSystem)
        {
            var startTime = new DateTime(2020, 1, 1);

            for (int day = 0; day < 10; day++)
            {
                CreateMockFile(folder, fileSystem, startTime, day);
            }

            return startTime;
        }


        private static void CreateMockFile(
            string folder,
            MockFileSystem fileSystem,
            DateTime startTime,
            int day)
        {
            var mockFile = new MockFileData("")
            {
                CreationTime = startTime + TimeSpan.FromDays(day),
            };

            fileSystem.AddFile(
                path: System.IO.Path.Combine(folder, $"{day}.txt"),
                mockFile: mockFile);
        }


        private static MockFileSystem CreateVirtualFileSystemWithFolder(string folder)
        {
            var fileSystem = new MockFileSystem();
            fileSystem.AddDirectory(folder);
            return fileSystem;
        }
    }
}
