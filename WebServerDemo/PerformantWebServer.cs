namespace WebServerDemo;

public class PerformantWebServer
{
    private Queue<string?> requestQueue = new();


    public void Demo()
    {
        //2. Start the requests queue monitoring thread
        Thread monitoringThread = new Thread(MonitorQueue);
        monitoringThread.Start();

        //1. Enqueue the incoming requests
        while (true)
        {
            string? input = Console.ReadLine();
            if (input?.ToLower() == "exit")
            {
                break;
            }

            requestQueue.Enqueue(input);
        }
    }

    void MonitorQueue()
    {
        while (true)
        {
            if (requestQueue.Count > 0)
            {
                string? input = requestQueue.Dequeue();
                Thread processingThread = new Thread(() => ProcessInput(input));
                processingThread.Start();
            }
            Thread.Sleep(100);
        }
    }

    //3. Processing the requests
    void ProcessInput(string? input)
    {
        Thread.Sleep(2000);//simulate processing time
        Console.WriteLine($"Processed input {input}");
    }
}