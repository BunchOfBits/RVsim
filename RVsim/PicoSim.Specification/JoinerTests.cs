using NUnit.Framework;

namespace PicoSim.Specification;

public class JoinerTests
{
  [TestCase(42UL)]
  [TestCase(65535UL)]
  [TestCase(1_000_000_000_000_000_000UL)]
  public void Check64Bit(ulong x)
  {
    // Arrange
    var d = new Source<ulong>("D");
    var splitter = new Splitter<ulong>("Splitter", d.Port);
    var dut = new Joiner<ulong>("Joiner", splitter.Q);

    // Act
    d.Set(x);

    // Assert
    Assert.That(dut.Q.Value, Is.EqualTo(x));
  }

  [TestCase(42U)]
  [TestCase(65535U)]
  [TestCase(1_000_000_000U)]
  public void Check32Bit(uint x)
  {
    // Arrange
    var d = new Source<uint>("D");
    var splitter = new Splitter<uint>("Splitter", d.Port);
    var dut = new Joiner<uint>("Joiner", splitter.Q);

    // Act
    d.Set(x);

    // Assert
    Assert.That(dut.Q.Value, Is.EqualTo(x));
  }

  [TestCase((ushort)42)]
  [TestCase((ushort)65535)]
  [TestCase((ushort)10_000)]
  public void Check16Bit(ushort x)
  {
    // Arrange
    var d = new Source<ushort>("D");
    var splitter = new Splitter<ushort>("Splitter", d.Port);
    var dut = new Joiner<ushort>("Joiner", splitter.Q);

    // Act
    d.Set(x);

    // Assert
    Assert.That(dut.Q.Value, Is.EqualTo(x));
  }

  [TestCase((byte)42)]
  [TestCase((byte)255)]
  [TestCase((byte)100)]
  public void Check8Bit(byte x)
  {
    // Arrange
    var d = new Source<byte>("D");
    var splitter = new Splitter<byte>("Splitter", d.Port);
    var dut = new Joiner<byte>("Joiner", splitter.Q);

    // Act
    d.Set(x);

    // Assert
    Assert.That(dut.Q.Value, Is.EqualTo(x));
  }
}