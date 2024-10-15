using AutoResetEvent autoResetEvent = new AutoResetEvent(false);

Console.WriteLine("Server is running. Type 'go' to proceed!");

//start the worker thread 
for (int i = 0; i < 3; i++)
{
    Thread workerThread = new Thread(Worker);
    workerThread.Name = $"Worker {i + 1}";
    workerThread.Start();
}

//Main thread receives user input and sends signals
while (true)
{
    var userInput = Console.ReadLine() ?? "";

    //Signal the worker thread if the input is 'go'

    if (userInput.ToLower() == "go")
    {
        autoResetEvent.Set();
    }
}


void Worker()
{
    while (true)
    {
        Console.WriteLine($"Worker {Thread.CurrentThread.Name} is waiting for the signal");

        autoResetEvent.WaitOne();

        Console.WriteLine($"Worker {Thread.CurrentThread.Name} threads proceeds");

        Thread.Sleep(3000);// Simulates working time
    }
}