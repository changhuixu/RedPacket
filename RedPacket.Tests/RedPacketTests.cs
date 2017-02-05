using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace RedPacket.Tests
{
    [TestFixture]
    public class RedPacketTests
    {
        private readonly Random _rand = new Random();

        [Test]
        public void ShouldSumUpToTotalAfterRandomization()
        {
            for (var i = 0; i < 10000000; i++)
            {
                decimal total = _rand.Next(1, 1001);
                var numberOfPackets = _rand.Next(1, 50);
                var test = new RedPackets(total, numberOfPackets);
                test.Packets.Sum(x => x).Should().Be(total);
            }
        }
    }
}
