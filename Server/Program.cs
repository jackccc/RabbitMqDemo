using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("消费者1");
            Console.WriteLine();
            Console.WriteLine();

            var queueProcessor = new RabbitConsumer {Enabled = true};
            queueProcessor.Start();
            Console.ReadLine();
        }
    }
}
