
#region Example3MoreSubtle

//Consider a more subtle case of a deadlock-prone locking protocol:

#endregion
void Transfer(Account a, Account b, decimal amount)
{
    lock (a)
    {
        lock (b)
        {
            if (a.Balance < amount)
                throw new InsufficientFundsException();
            a.Balance -= amount;
            b.Balance += amount;
        }
    }
}


//If somebody tried transferring, say, $500 from account #1234 to account #5678 
//at the same time somebody else tried transferring $1,000 from account #5678 to account #1234,
//a deadlock becomes highly probable. 
//    Reference aliasing such as this, where multiple threads refer to the same object using
//    different variables, can cause headaches and is an extraordinarily common practice. 

#region Example2

object lock1 = new object();
object lock2 = new object();
Thread thread1 = new Thread(ExecuteThread1);
Thread thread2 = new Thread(ExecuteThread2);
thread1.Start();
thread2.Start();
thread1.Join();
thread2.Join();

Console.WriteLine("Main program has finished.");

void ExecuteThread1()
{
    lock (lock1)
    {
        Console.WriteLine("Thread 1: Holding lock1...");
        Thread.Sleep(100);

        // Waiting for lock2 with timeout 1 millisecond
        if (Monitor.TryEnter(lock2, TimeSpan.FromMilliseconds(1)))
        {
            try
            {
                Console.WriteLine("Thread 1: Acquired lock2!");
            }
            finally
            {
                Monitor.Exit(lock2);
            }
        }
        else
        {
            Console.WriteLine("Thread 1: Failed to acquire lock2 within 1 millisecond.");
        }
    }
}

void ExecuteThread2()
{
    lock (lock2)
    {
        Console.WriteLine("Thread 2: Holding lock2...");
        Thread.Sleep(100);

        // Waiting for lock1 with timeout 1 millisecond
        if (Monitor.TryEnter(lock1, TimeSpan.FromMilliseconds(1)))
        {
            try
            {
                Console.WriteLine("Thread 2: Acquired lock1!");
            }
            finally
            {
                Monitor.Exit(lock1);
            }
        }
        else
        {
            Console.WriteLine("Thread 2: Failed to acquire lock1 within 1 millisecond.");
        }
    }
}

//Explanation: In this example, thread1 attempts to acquire lock1, but with a timeout of 1 second. If it fails to acquire the lock within that time, it proceeds with the "else" block and releases the lock.
#endregion

//#region Example1

////e - commerce: users and orders
////1. managing users (user -> order)
////2. managing orders (order->user)

////Thread 1 wants to lock user first then lock order
//// Thread 2 wants to lock order first then lock the user


//object userLock = new();
//object orderLock = new();


//Thread thread = new Thread(ManageOrder);
//thread.Start();

//ManageUser();
//thread.Join();

//Console.ReadLine();
//Console.WriteLine("Program finished!"); // never reach this due to a deadlock situation
//void ManageUser()
//{
//    lock (userLock)
//    {
//        Console.WriteLine("User management acquired the user lock");
//        Thread.Sleep(2000);

//        lock (orderLock)
//        {
//            Console.WriteLine("Order management acquired the order lock");
//        }
//    }
//}

//void ManageOrder()
//{
//    lock (orderLock)
//    {
//        Console.WriteLine("Order management acquired the order lock");
//        Thread.Sleep(1000);

//        lock (userLock)
//        {
//            Console.WriteLine("User management acquired the order lock");
//        }
//    }
//}

//#endregion