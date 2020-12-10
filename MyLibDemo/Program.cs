using System;
using System.IO;


/// <summary>Date time services using real .Net DateTime</summary>
System.Abstractions.DateTimeServices dateTimeServices = new System.Abstractions.DateTimeServices();


// Demos!
//SimpleTimerDemo();
FolderTidyDemo();


/// <summary>
/// Demo of the simple timer, using real .Net DateTime
/// </summary>
void SimpleTimerDemo()
{
    Say(nameof(SimpleTimerDemo));

    MyLib.SimpleTimer simpleTimer = new MyLib.SimpleTimer(
        dateTimeServices: dateTimeServices,
        interval: TimeSpan.FromSeconds(4));


    while (!simpleTimer.Expired)
    {
        Say("Waiting for timer to expire");

        System.Threading.Thread.Sleep(500);
    }

    Say($"Timer has expired");
}


/// <summary>
/// Demo's the FolderTidy class. Use this interactively with the debugger
/// to see the files in the folder, and to then see them deleted etc.
/// </summary>
void FolderTidyDemo()
{
    // Create a folder
    var folder = Directory.GetCurrentDirectory();
    folder = Path.Combine(folder, "FolderTidyDemo");
    Directory.CreateDirectory(folder);

    // Add 10 tiles, 100ms apart
    for(int index = 0; index < 10; index++)
    {
        var fileName = Path.Combine(folder, $"{index}.txt");
        File.WriteAllText(fileName, $"{index}");
        System.Threading.Thread.Sleep(100);
    }

    // Test our function using the real file system
    MyLib.FolderTidy.DeleteOldFiles(
        folder: folder,
        oldestAgeToKeep: TimeSpan.FromMilliseconds(1500),
        fileSystem: new System.IO.Abstractions.FileSystem(),
        dateTimeServices: new System.Abstractions.DateTimeServices());

    // Scap remaining files
    foreach(var file in Directory.GetFiles(folder))
    {
        File.Delete(file);
    }

    // Scrap the test folder
    Directory.Delete(folder);
}



/// <summary>
/// Send a message to the output, prefixed with current UTC time
/// </summary>
void Say(string msg)
{
    Console.WriteLine($"{dateTimeServices.UtcNow:HH.mm.ss}: {msg}");
}
