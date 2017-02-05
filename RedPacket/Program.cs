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
        }
    }
}