using HalloSingelton;

Console.WriteLine("Hello Singelton");

//var log = new Logger();

for (int i = 0; i < 10; i++)
{
    Task.Run(() => Logger.Instance.Info("Hallo Logger"));
}

Logger.Instance.Info("Mehr Log infos");