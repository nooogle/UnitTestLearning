using System;


/// <summary>Date time services using real .Net DateTime</summary>
System.Abstractions.DateTimeServices dateTimeServices = new System.Abstractions.DateTimeServices();


// Demos!
SimpleTimerDemo();


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
/// Send a message to the output, prefixed with current UTC time
/// </summary>
void Say(string msg)
{
    Console.WriteLine($"{dateTimeServices.UtcNow:HH.mm.ss}: {msg}");
}
