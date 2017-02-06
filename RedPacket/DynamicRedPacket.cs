using System;

namespace RedPacket
{
    public class DynamicRedPacket
    {
        public double RemainMoney => _totalMoneyAllowToBeRandomized + RemainCount * Min;
        public int RemainCount { get; private set; }
        private const double Min = 0.01;
        private double _totalMoneyAllowToBeRandomized;

        public DynamicRedPacket(double totalMoney, int nPackets)
        {
            RemainCount = nPackets;
            _totalMoneyAllowToBeRandomized = totalMoney - nPackets * Min;
        }

        public double GetRedPacket()
        {
            if (RemainCount == 1) return RemainMoney;
            var rand = new Random();
            var max = _totalMoneyAllowToBeRandomized / RemainCount * 2;
            var money = Math.Round(rand.NextDouble() * max, 2);
            RemainCount--;
            _totalMoneyAllowToBeRandomized -= money;
            return money + Min;
        }
    }
}
