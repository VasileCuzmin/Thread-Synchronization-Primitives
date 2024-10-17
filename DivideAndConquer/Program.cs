

int[] arr = new int[1000000];

for (int i = 0; i < 1000000; i++)
{
    arr[i] = i;
}

int SumSegment(int start, int end)
{
    int sum = 0;
    for (int i = start; i < end; i++)
    {
        sum += arr[i];
    }

    return sum;
}


int sum1 = 0, sum2 = 0, sum3 = 0, sum4 = 0;

int[] sumArr = new int[4];

int sum = 0;
int numOfThreads = 4;
int segmentLength = arr.Length / numOfThreads;


Thread[] threads = new Thread[numOfThreads];

//threads[0] = new Thread(() => { sum1 = SumSegment(0, segmentLength); });
//threads[1] = new Thread(() => { sum2 = SumSegment(segmentLength, 2 * segmentLength); });
//threads[2] = new Thread(() => { sum3 = SumSegment(2 * segmentLength, 3 * segmentLength); });
//threads[3] = new Thread(() => { sum4 = SumSegment(3 * segmentLength, arr.Length); });

for (int i = 0; i < threads.Length; i++)
{
    int localI = i;
    threads[i] = new Thread(() => { sumArr[localI] = SumSegment(localI * segmentLength, (localI + 1) * segmentLength); });
}

var startTime = DateTime.Now;
foreach (var item in threads) { item.Start(); }
foreach (var item in threads) { item.Join(); }

var endTime = DateTime.Now;

var timespan = endTime - startTime;

Console.WriteLine(sumArr.Sum());
Console.WriteLine(timespan.TotalSeconds);


//var startTime1 = DateTime.Now;
//for (int i = 0; i < arr.Length; i++)
//{
//    sum += arr[i];
//}

//var endTime1 = DateTime.Now;

//var timespan1 = endTime1 - startTime1;

//Console.WriteLine(sum);
//Console.WriteLine(timespan1.TotalSeconds);