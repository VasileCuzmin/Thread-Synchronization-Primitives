int counter = 0;//shared resource between multiple threads
object objLock = new();

Thread thread1 = new Thread(IncrementCounter);
thread1.Start();

Thread thread2 = new Thread(IncrementCounter);
thread2.Start();


thread1.Join();
thread2.Join();

Console.WriteLine($"Final counter value is {counter}");


void IncrementCounter()
{
    //for (int i = 0; i < 100000; i++)
    //{
    //    counter += 1;//non-deterministic result each time - critical section - race condition
    //}

    for (int i = 0; i < 100000; i++)
    {
        lock (objLock)
        {
            counter += 1;
        }

        //Interlocked.Increment(ref counter); // the same as the below
    }
}