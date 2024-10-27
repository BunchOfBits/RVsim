using NUnit.Framework;
using PicoSim;
using RVsim.EXE.ALU;

namespace RVsim.Specification.EXE.ALU;

public class FullAdderBitSliceTests
{
  private Source<bool> _a;
  private Source<bool> _b;
  private Source<bool> _ci;
  private FullAdderBitSlice _dut;

  [SetUp]
  public void SetUp()
  {
    _a = new Source<bool>("A");
    _b = new Source<bool>("B");
    _ci = new Source<bool>("Ci");

    _dut = new FullAdderBitSlice("FullAdder", _a.Port, _b.Port, _ci.Port);
  }

  [TestCase(false, false, false, false, false)]
  [TestCase(false, false, true, false, true)]
  [TestCase(false, true, false, false, true)]
  [TestCase(false, true, true, true, false)]
  [TestCase(true, false, false, false, true)]
  [TestCase(true, false, true, true, false)]
  [TestCase(true, true, false, true, false)]
  [TestCase(true, true, true, true, true)]
  public void CheckFullAdder(bool a, bool b, bool ci, bool co, bool s)
  {
    // Arrange
    _a.Set(a);
    _b.Set(b);
    _ci.Set(ci);

    // Act

    // Assert
    Assert.That(_dut.Co.Value, Is.EqualTo(co));
    Assert.That(_dut.Sum.Value, Is.EqualTo(s));
  }
}