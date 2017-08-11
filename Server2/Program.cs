using System;

namespace Server2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("消费者2");
            Console.WriteLine();
            Console.WriteLine();

            var queueProcessor = new RabbitConsumer {Enabled = true};
            queueProcessor.Start();
            Console.ReadLine();
        }
    }
}
