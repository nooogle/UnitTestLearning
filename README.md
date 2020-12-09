# UnitTestLearning

* Scratch pad for playing with and learning unit testing and associated technologies
* .Net 5
* Visual Studio on OSX and Windows 10


Key NuGet libraries being used:

* [XUnit](https://xunit.net)
* [NSubstitute](https://nsubstitute.github.io) (preferred over Moq)
* [FluentAssertions](https://fluentassertions.com), extension methods to allow for more declarative/expressive testing of outcomes
* [System.IO.Abstractions](https://github.com/System-IO-Abstractions/System.IO.Abstractions), for real/mocked file system calls

This is also a scratchpad for playing with interface-based access to 
otherwise static system libraries. E.g. unit testing with DateTime is a 
pain, especially if intervals of time are required. Using an abstract, 
mockable date/time prover

