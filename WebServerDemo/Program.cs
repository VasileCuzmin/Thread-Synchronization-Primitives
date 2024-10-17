//https://www.youtube.com/watch?v=KyOuSMc17Mk&list=PLgRlicSxjeMMYTN01qiQ10aLKEWeSEs-v&index=18

using WebServerDemo;

Console.WriteLine("Server is running. Type 'exit' to stop.");

//PoorWebServer poorWebServer = new PoorWebServer(); // in this case the input is blocked
//and processing one by one 
//poorWebServer.Demo();


//in this case the input is not being blocked and is processed one by one in an async manner
PerformantWebServer performantWebServer = new PerformantWebServer();
performantWebServer.Demo();