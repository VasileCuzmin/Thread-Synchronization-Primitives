//https://www.youtube.com/watch?v=ynwzwBjkWEA&list=PLgRlicSxjeMMYTN01qiQ10aLKEWeSEs-v&index=5

using System.Collections.Concurrent;

ConcurrentQueue<int> queue = new();
var consumeEvent = new ManualResetEventSlim(false);
var produceEvent = new ManualResetEventSlim(true);

int consumerCount = 0;
object lockConsumerCount = new object();

Thread[] consumerThreads = new Thread[3];
for (int i = 0; i < 3; i++)
{
    consumerThreads[i] = new Thread(Consume)
    {
        Name = $"Consumer {i+1}"
    };
    consumerThreads[i].Start();
}

//Producer
while (true)
{
    produceEvent.Wait();
    produceEvent.Reset();//signalled=false - we dont want to produce again until the queue is empty

    Console.WriteLine("To produce, enter 'p'");
    var input = Console.ReadLine() ?? "";

    if (input.ToLower() == "p")
    {
        for (int i = 1; i <= 10; i++)
        {
            queue.Enqueue(i);
            Console.WriteLine($"Produced {i}");
        }

        consumeEvent.Set();//signalled=true
    }
}

//Consumer's behavior
void Consume()
{
    while (true)
    {
        consumeEvent.Wait();

        while (queue.TryDequeue(out int item))
        {
            //work on the items produced

            Thread.Sleep(1500);

            Console.WriteLine($"Cosumed: {item} from thread {Thread.CurrentThread.Name}");
        }

        lock (lockConsumerCount)
        {
            consumerCount++;
            if (consumerCount == 3) //we have 3 consumer threads
            {
                consumeEvent.Reset(); //turn off the signal for the consumers so that the producer could have enough time to produce
                produceEvent.Set(); //produce again if the queue is empty
                consumerCount = 0;

                Console.WriteLine("*******************");
                Console.WriteLine("More please!");
                Console.WriteLine("*******************");
            }
        }
    }
}