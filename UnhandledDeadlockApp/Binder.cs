using System;
using System.Threading;

namespace UnhandledDeadlockApp
{
    public class Binder
    {
        private readonly object _gate = new object();

        public void UpdateGrid()
        {
            lock (_gate)
            {
                Console.WriteLine(@"Entered!");
            }
        }

        public void EndUpdate()
        {
            Monitor.Enter(_gate);
            try
            {
                throw new Exception("My Unhandled exception!");
            }
            finally
            {
                Monitor.Exit(_gate);
            }
        }
    }
}
