using System;

namespace RedPacket
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new RedPackets(50.0m, 8);
            foreach (var packet in test.Packets)
            {
                Console.WriteLine(packet);
            }
            Console.ReadKey();

            Console.WriteLine("Dynamic Red Packets");
            const double total = 50.0;
            const int num = 8;
            var test2 = new DynamicRedPacket(total, num);
            for (var i = 0; i < num; i++)
            {
                Console.ReadKey();
                Console.WriteLine(i + "\t" + test2.GetRedPacket());
            }
            Console.WriteLine("Dynamic Red Packets Out");
            Console.ReadKey();
        }
    }
}