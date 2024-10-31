using NUnit.Framework;
using PicoSim;

namespace RVsim.Specification.EXE.BarrelShifter;

public class BarrelShifterTests
{
  private Source<uint> _d;
  private Source<byte> _amount;
  private Source<bool> _right;
  private Source<bool> _arithmetic;

  private RVsim.EXE.BarrelShifter.BarrelShifter _dut;

  [SetUp]
  public void Setup()
  {
    _d = new Source<uint>("D");
    _amount = new Source<byte>("Amount");
    _right = new Source<bool>("Right");
    _arithmetic = new Source<bool>("Arithmetic");

    _dut = new RVsim.EXE.BarrelShifter.BarrelShifter("Shift", _d.Port, _amount.Port, _right.Port, _arithmetic.Port);
  }

  [TestCase(0xCA0F_E1E6U, 1, true, false, 0x6507_F0F3U)]  // LSR(1)
  [TestCase(0xCA0F_E1E6U, 1, true, true, 0xE507_F0F3U)]   // ASR(1)
  [TestCase(0xCA0F_E1E6U, 1, false, false, 0x941F_C3CCU)] // LSL(1)
  [TestCase(0xCA0F_E1E6U, 1, false, true, 0x941F_C3CCU)]  // LSL(1)
  [TestCase(0x4A0F_E1E6U, 1, true, false, 0x2507_F0F3U)]  // LSR(1)
  [TestCase(0x4A0F_E1E6U, 1, true, true, 0x2507_F0F3U)]   // ASR(1)
  [TestCase(0x4A0F_E1E6U, 1, false, false, 0x941F_C3CCU)] // LSL(1)
  [TestCase(0x4A0F_E1E6U, 1, false, true, 0x941F_C3CCU)]  // LSL(1)
  public void CheckTheOperatingModes(uint d, byte amount, bool right, bool arithmetic, uint result)
  {
    // Arrange
    _d.Set(d);
    _amount.Set(amount);
    _right.Set(right);
    _arithmetic.Set(arithmetic);

    // Act

    // Assert
    Assert.That(_dut.Q.Value, Is.EqualTo(result));
  }
}