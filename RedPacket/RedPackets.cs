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
        private readonly int _numOfPackets;
        public List<decimal> Packets { get; }
        public RedPackets(decimal totalMoney, int numOfPackets, decimal minMoneyForEachPacket = 0.01m)
        {
            _rand = new Random();
            _totalMoney = totalMoney;
            _numOfPackets = numOfPackets;
            _minMoneyForEachPacket = minMoneyForEachPacket;

            EnsureEachPacketHasMinMoney();
            Packets = Randomize();
        }

        private void EnsureEachPacketHasMinMoney()
        {
            if (_numOfPackets * _minMoneyForEachPacket > _totalMoney)
            {
                throw new ArgumentException($"Total money is not enough for {_numOfPackets} packets.");
            }
        }

        private List<decimal> GetRandomNumberList()
        {
            var packets = new List<decimal>();
            for (var i = 0; i < _numOfPackets; i++)
            {
                var randomNumber = _rand.Next(0, (int)_totalMoney * 100 / _numOfPackets);
                packets.Add(randomNumber);
            }
            return packets;
        }
        private List<decimal> RandomizeWithOnlyOneLuckestDraw()
        {
            var packets = GetRandomNumberList();
            while (true)
            {
                var max = packets.Max();
                if (packets.Count(x => x == max) == 1) break;
                packets = GetRandomNumberList();
            }
            return packets;
        }

        private List<decimal> Randomize()
        {
            var remain = _totalMoney - _minMoneyForEachPacket * _numOfPackets;
            var packets = RandomizeWithOnlyOneLuckestDraw();
            var ratio = packets.Sum(x => x) / remain;
            packets = packets.Select(x => decimal.Round(x / ratio, 2) + _minMoneyForEachPacket).ToList();
            var difference = packets.Sum(x => x) - _totalMoney;
            if (Math.Abs(difference) >= 0.01m) packets[_rand.Next(0, _numOfPackets)] -= difference;
            return packets;
        }
    }
}
