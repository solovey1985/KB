using System;
using System.Threading;

/*
	ManualResetEvent
	
	ManualResetEvent allows threads to interact with each other by sending signals. 
	This interaction usually concerns a task that one thread must complete before another can continue its work. 
	When a thread starts a task that must be completed before other threads can continue, 
	it calls the Reset method to put the ManualResetEvent into a non-signaled state. 
	This thread can be understood as controlling the ManualResetEvent. 
	Threads that call the WaitOne method on the ManualResetEvent will be blocked, waiting for a signal. 
	When the controlling thread completes its task, it will call the Set method to signal that the waiting threads can continue. 
	All waiting threads are released.
*/

namespace ManualResetEventNs
{
	class Program
	{
		// ManualResetEvent - Notifies one or more waiting threads that an event has occurred.
	    private static ManualResetEvent manual = new ManualResetEvent(false);

		static void Main()
		{
			Console.WriteLine("Press any key to set the ManualResetEvent to the signaled state.\n");

			Thread[] threads = { new Thread(Function1), new Thread(Function2) };

			foreach (Thread thread in threads)
				thread.Start();

			Console.ReadKey();
			manual.Set(); // Sends a signal to all threads.
    
			// Delay.
			Console.ReadKey();
		}

		static void Function1()
		{
			Console.WriteLine("Thread 1 started and is waiting for a signal.");
			manual.WaitOne(); // After WaitOne() completes, ManualResetEvent remains in the signaled state.
			Console.WriteLine("Thread 1 is terminating.");
		    
		}

		static void Function2()
		{
			Console.WriteLine("Thread 2 started and is waiting for a signal.");
			manual.WaitOne(); // After WaitOne() completes, ManualResetEvent remains in the signaled state.
			Console.WriteLine("Thread 2 is terminating.");
		}
	}
}
