using ManualResetEventSlim manualResetEventSlim = new ManualResetEventSlim(false);

Console.WriteLine("Press enter to release all threads...");

//start the worker thread 
for (int i = 0; i < 3; i++)
{
    Thread workerThread = new Thread(Work);
    workerThread.Name = $"Worker {i + 1}";
    workerThread.Start();
}

Console.ReadLine();

manualResetEventSlim.Set();

Console.ReadLine();


void Work()
{
    Console.WriteLine($"{Thread.CurrentThread.Name} is waiting for the signal");

    manualResetEventSlim.Wait();

    Thread.Sleep(3000);// Simulates working time

    Console.WriteLine($"{Thread.CurrentThread.Name} has been released");
}