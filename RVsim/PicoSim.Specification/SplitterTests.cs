using NUnit.Framework;

namespace PicoSim.Specification;

public class SplitterTests
{
  [TestCase(42UL)]
  [TestCase(65535UL)]
  [TestCase(1_000_000_000_000_000_000UL)]
  public void Check64Bit(ulong x)
  {
    // Arrange
    var d = new Source<ulong>("D");
    var dut = new Splitter<ulong>("Splitter", d.Port);

    // Act
    d.Set(x);

    // Assert
    for (var i = 0; i < 64; i++)
    {
      Assert.That(dut.Q[i].Value, Is.EqualTo((x & (1UL << i)) != 0));
    }
  }

  [TestCase(42U)]
  [TestCase(65535U)]
  [TestCase(1_000_000_000U)]
  public void Check32Bit(uint x)
  {
    // Arrange
    var d = new Source<uint>("D");
    var dut = new Splitter<uint>("Splitter", d.Port);

    // Act
    d.Set(x);

    // Assert
    for (var i = 0; i < 32; i++)
    {
      Assert.That(dut.Q[i].Value, Is.EqualTo((x & (1 << i)) != 0));
    }
  }

  [TestCase((ushort)42)]
  [TestCase((ushort)65535)]
  [TestCase((ushort)10_000)]
  public void Check16Bit(ushort x)
  {
    // Arrange
    var d = new Source<ushort>("D");
    var dut = new Splitter<ushort>("Splitter", d.Port);

    // Act
    d.Set(x);

    // Assert
    for (var i = 0; i < 16; i++)
    {
      Assert.That(dut.Q[i].Value, Is.EqualTo((x & (1 << i)) != 0));
    }
  }

  [TestCase((byte)42)]
  [TestCase((byte)255)]
  [TestCase((byte)100)]
  public void Check8Bit(byte x)
  {
    // Arrange
    var d = new Source<byte>("D");
    var dut = new Splitter<byte>("Splitter", d.Port);

    // Act
    d.Set(x);

    // Assert
    for (var i = 0; i < 8; i++)
    {
      Assert.That(dut.Q[i].Value, Is.EqualTo((x & (1 << i)) != 0));
    }
  }
}