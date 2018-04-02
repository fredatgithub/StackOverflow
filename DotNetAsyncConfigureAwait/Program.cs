using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigureAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().GetAwaiter().GetResult();
        }

        static async Task Run()
        {
            var syncContext = new MySyncContext { State = "The Original Context" };
            SynchronizationContext.SetSynchronizationContext(syncContext);

            Console.WriteLine("Original:" + SynchronizationContext.Current);

            await Task.Delay(1000).ConfigureAwait(false);
            Console.WriteLine("After Result1:" + (SynchronizationContext.Current == null));

            await Task.Delay(1000);
            Console.WriteLine("After Result2:" + (SynchronizationContext.Current == null));
        }
    }

    public class MySyncContext : SynchronizationContext
    {
        public string State { get; set; }

        public override void Post(SendOrPostCallback callback, object state)
        {
            base.Post(s => { 
                SynchronizationContext.SetSynchronizationContext(this);
                callback(s);
            }, state);
        }

        public override string ToString() => State;
    }
}
