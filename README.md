# Simple Thread Example in .NET Core, using C#

Simple example of threads with a mutex.

Have interactive option of causing thread to throw an exception.

Created on Windows 10, using dotnet version 1.0.0-preview2-003131 and
VisualStudio 2015 release 3.

See http://dot.net for help installing .NET Core on Windows, Mac OSX, or Linux.

## Build and Run

To build:

1. `git clone git@github.com:rstinejr/SimpleCSharpThreads.git`
2. `cd SimpleCSharpThreads`
3. `cd proj`
4. `dotnet restore`
5. `dotnet build`

To run, `dotnet run`

Alternatively, if you have Visual Studio, open the solution file.

Expected output from `dotnet run` if no exception is thrown by thread:

```
Project proj (.NETCoreApp,Version=v1.0) was previously compiled. Skipping compilation.
Main thread 1, create a worker thread.
Start thread
Thread 3 started.
Press 'x' to throw exception in thread, any other key to continue: a
Thread 3 has mutex. Do work, release mutex, exit.
Thread joined. Done.
```

Expected output when thread throws an exception:

```
Project proj (.NETCoreApp,Version=v1.0) was previously compiled. Skipping compilation.
Main thread 1, create a worker thread.
Start thread
Thread 3 started.
Press 'x' to throw exception in thread, any other key to continue: x
Thread 3 done. Throw an exception.
Exception caught in thread 3
Thread joined. Done.
```

## Exceptions in Theads

If you play with this code a bit, you will see that an unhandled exception in the
worker thread will cause the program to crash, but you cannot catch the 
worker thread's exception in the main thread.  The catch-block needs to be 
run from the worker thread if the exception is to be caught.
