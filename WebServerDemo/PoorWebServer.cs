namespace WebServerDemo;

public class PoorWebServer
{

    //The whole processing is not performant. We have to wait 2 seconds for each incoming request

    public void Demo()
    {
        while (true)
        {
            string? input = Console.ReadLine();
            if (input?.ToLower() == "exit")
            {
                break;
            }

            ProcessInput(input);
        }
    }

    void ProcessInput(string? input)
    {
        Thread.Sleep(2000);//simulate processing time
        Console.WriteLine($"Processed input {input}");
    }
}