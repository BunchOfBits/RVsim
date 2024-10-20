using PicoSim;
using NUnit.Framework;

namespace RVsim.Specification.EXE.BarrelShifter
{
  public class BarrelShifterTests
  {
    private Source<uint> _d;
    private Source<byte> _amount;
    private Source<bool> _right;
    private Source<bool> _arithmetic;
    private Source<bool> _clk;

    private RVsim.EXE.BarrelShifter.BarrelShifter _dut;

    [SetUp]
    public void Setup()
    {
      _d = new Source<uint>("D");
      _amount = new Source<byte>("Amount");
      _right = new Source<bool>("Right");
      _arithmetic = new Source<bool>("Arithmetic");
      _clk = new Source<bool>("Clk");

      _dut = new RVsim.EXE.BarrelShifter.BarrelShifter("Shift", _d.Port, _amount.Port, _right.Port, _arithmetic.Port, _clk.Port);
    }

    [Test]
    public void CheckThatBarrelShifterOutputInitializesToZero()
    {
      // Arrange
      _d.Set(65536);
      _amount.Set(16);
      _right.Set(true);
      _arithmetic.Set(false);

      // Act

      // Assert
      Assert.That(_dut.Q.Value, Is.EqualTo(0));
    }

    [TestCase(0xCA0F_E1E6u, 1, true, false, 0x6507_F0F3u)]
    public void CheckThatTheFirstStageShiftsBySixteen(uint d, byte amount, bool right, bool arithmetic, uint result)
    {
      // Arrange
      _d.Set(d);
      _amount.Set(amount);
      _right.Set(right);
      _arithmetic.Set(arithmetic);

      // Act
      _clk.Set(true);
      _clk.Set(false);
      _clk.Set(true);
      _clk.Set(false);
      _clk.Set(true);
      _clk.Set(false);
      _clk.Set(true);
      _clk.Set(false);
      _clk.Set(true);
      _clk.Set(false);

      // Assert
      Assert.That(_dut.Q.Value, Is.EqualTo(result));
    }

    [Test]
    public void CheckThatBarrelShifterPipeline()
    {
      // Arrange
      _d.Set(0xCA0F_E1E6u);
      _amount.Set(4);
      _right.Set(true);
      _arithmetic.Set(true);

      // Act
      _clk.Set(true);   // ASR(4)
      _clk.Set(false);

      _amount.Set(8);

      _clk.Set(true);   // ASR(8)
      _clk.Set(false);

      _right.Set(false);

      _clk.Set(true);   // LSL(8)
      _clk.Set(false);

      _arithmetic.Set(false);

      _clk.Set(true);   // LSL(8)
      _clk.Set(false);

      _right.Set(true);

      _clk.Set(true);   // LSR(8)
      _clk.Set(false);

      // Assert
      Assert.That(_dut.Q.Value, Is.EqualTo(0xFCA0_FE1Eu));

      _clk.Set(true);
      _clk.Set(false);

      Assert.That(_dut.Q.Value, Is.EqualTo(0xFFFC_A0FEu));

      _clk.Set(true);
      _clk.Set(false);

      Assert.That(_dut.Q.Value, Is.EqualTo(0xFCA0_FE00u));

      _clk.Set(true);
      _clk.Set(false);

      Assert.That(_dut.Q.Value, Is.EqualTo(0xA0FE_0000u));

      _clk.Set(true);
      _clk.Set(false);

      Assert.That(_dut.Q.Value, Is.EqualTo(0x00A0_FE00u));
    }
  }
}
