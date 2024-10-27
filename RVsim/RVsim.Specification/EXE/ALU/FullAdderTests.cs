using NUnit.Framework;

using PicoSim;

using RVsim.EXE.ALU;

namespace RVsim.Specification.EXE.ALU;

public class FullAdderTests
{
  [TestCase(21UL, 21UL, false, false, 42UL)]
  [TestCase(255UL, 1UL, false, false, 256UL)]
  [TestCase(255UL, 0UL, true, false, 256UL)]
  [TestCase(0xffff_ffff_ffff_ffffUL, 1UL, false, true, 0UL)]
  [TestCase(0xffff_ffff_ffff_ffffUL, 0UL, true, true, 0UL)]
  [TestCase(0x9999_9999_9999_9999UL, 0xffff_ffff_ffff_ffffUL, false, true, 0x9999_9999_9999_9998UL)]
  public void Check64BitFullAdder(ulong a, ulong b, bool ci, bool co, ulong s)
  {
    var A = new Source<ulong>("A");
    var B = new Source<ulong>("B");
    var Ci = new Source<bool>("Ci");
    var dut = new FullAdder<ulong>("FullAdder", A.Port, B.Port, Ci.Port);

    // Arrange
    A.Set(a);
    B.Set(b);
    Ci.Set(ci);

    // Act

    // Assert
    Assert.That(dut.Co.Value, Is.EqualTo(co));
    Assert.That(dut.Sum.Value, Is.EqualTo(s));
  }

  [TestCase(21U, 21U, false, false, 42U)]
  [TestCase(255U, 1U, false, false, 256U)]
  [TestCase(255U, 0U, true, false, 256U)]
  [TestCase(0xffff_ffffU, 1U, false, true, 0U)]
  [TestCase(0xffff_ffffU, 0U, true, true, 0U)]
  [TestCase(0x9999_9999U, 0xffff_ffffU, false, true, 0x9999_9998U)]
  public void Check32BitFullAdder(uint a, uint b, bool ci, bool co, uint s)
  {
    var A = new Source<uint>("A");
    var B = new Source<uint>("B");
    var Ci = new Source<bool>("Ci");
    var dut = new FullAdder<uint>("FullAdder", A.Port, B.Port, Ci.Port);

    // Arrange
    A.Set(a);
    B.Set(b);
    Ci.Set(ci);

    // Act

    // Assert
    Assert.That(dut.Co.Value, Is.EqualTo(co));
    Assert.That(dut.Sum.Value, Is.EqualTo(s));
  }

  [TestCase((ushort)21, (ushort)21, false, false, (ushort)42)]
  [TestCase((ushort)255, (ushort)1, false, false, (ushort)256)]
  [TestCase((ushort)255, (ushort)0, true, false, (ushort)256)]
  [TestCase((ushort)0xffff, (ushort)1, false, true, (ushort)0)]
  [TestCase((ushort)0xffff, (ushort)0, true, true, (ushort)0)]
  [TestCase((ushort)0x9999, (ushort)0xffff, false, true, (ushort)0x9998)]
  public void Check16BitFullAdder(ushort a, ushort b, bool ci, bool co, ushort s)
  {
    var A = new Source<ushort>("A");
    var B = new Source<ushort>("B");
    var Ci = new Source<bool>("Ci");
    var dut = new FullAdder<ushort>("FullAdder", A.Port, B.Port, Ci.Port);

    // Arrange
    A.Set(a);
    B.Set(b);
    Ci.Set(ci);

    // Act

    // Assert
    Assert.That(dut.Co.Value, Is.EqualTo(co));
    Assert.That(dut.Sum.Value, Is.EqualTo(s));
  }

  [TestCase((byte)21, (byte)21, false, false, (byte)42)]
  [TestCase((byte)0xff, (byte)1, false, true, (byte)0)]
  [TestCase((byte)0xff, (byte)0, true, true, (byte)0)]
  [TestCase((byte)0x99, (byte)0xff, false, true, (byte)0x98)]
  public void Check8BitFullAdder(byte a, byte b, bool ci, bool co, byte s)
  {
    var A = new Source<byte>("A");
    var B = new Source<byte>("B");
    var Ci = new Source<bool>("Ci");
    var dut = new FullAdder<byte>("FullAdder", A.Port, B.Port, Ci.Port);

    // Arrange
    A.Set(a);
    B.Set(b);
    Ci.Set(ci);

    // Act

    // Assert
    Assert.That(dut.Co.Value, Is.EqualTo(co));
    Assert.That(dut.Sum.Value, Is.EqualTo(s));
  }
}