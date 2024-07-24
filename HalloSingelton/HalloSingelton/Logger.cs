namespace HalloSingelton
{
    internal class Logger
    {
        private static Logger _instance = null;
        private static object _instanceLock = new object();
        public static Logger Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                        _instance = new Logger();
                }
                return _instance;
            }
        }

        private Logger()
        {
            Info("Neuer Logger");
        }

        public void Info(string msg)
        {
            Console.WriteLine($"{DateTime.Now:g} [INFO] {msg}");
        }

        public void Error(string msg)
        {
            Console.WriteLine($"{DateTime.Now:g} [ERROR] {msg}");
        }
    }
}
