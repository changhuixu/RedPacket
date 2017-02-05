using System;
using System.Collections.Generic;
using System.Linq;

namespace RedPacket
{
    public class RedPackets
    {
        private readonly Random _rand;
        private readonly decimal _minMoneyForEachPacket;
        private readonly decimal _totalMoney;
        private readonly int _totalNumOfPacket;
        public List<decimal> Packets { get; }
        public RedPackets(decimal totalMoney, int totalNumOfPacket, decimal minMoneyForEachPacket = 0.01m)
        {
            _rand = new Random();
            _totalMoney = totalMoney;
            _totalNumOfPacket = totalNumOfPacket;
            _minMoneyForEachPacket = minMoneyForEachPacket;

            EnsureEachPacketHasMinMoney();
            Packets = RandomizeWithOnlyOneLuckestDraw();
        }

        private void EnsureEachPacketHasMinMoney()
        {
            if (_totalNumOfPacket * _minMoneyForEachPacket > _totalMoney)
            {
                throw new ArgumentException($"Total money is not enough for {_totalNumOfPacket} packets.");
            }
        }

        private List<decimal> RandomizeWithOnlyOneLuckestDraw()
        {
            var packets = Randomize();
            while (true)
            {
                var max = packets.Max();
                if (packets.Count(x => x == max) == 1) break;
                packets = Randomize();
            }
            return packets;
        }

        private List<decimal> Randomize()
        {
            var packets = new List<decimal>();
            for (var i = 0; i < _totalNumOfPacket; i++)
            {
                var randomNumber = _rand.Next(1, (int)_totalMoney * 100);
                packets.Add(randomNumber);
            }
            var ratio = packets.Sum(x => x) / _totalMoney;
            packets = packets.Select(x => decimal.Round(x / ratio, 2)).ToList();
            var difference = packets.Sum(x => x) - _totalMoney;
            packets[_totalNumOfPacket - 1] -= difference;
            return packets;
        }
    }
}
