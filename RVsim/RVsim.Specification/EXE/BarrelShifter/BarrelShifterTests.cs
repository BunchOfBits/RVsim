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

    private Sink<uint> _q;

    [SetUp]
    public void Setup()
    {
      _d = new Source<uint>("D");
      _amount = new Source<byte>("Amount");
      _right = new Source<bool>("Right");
      _arithmetic = new Source<bool>("Arithmetic");
      _clk = new Source<bool>("Clk");

      _dut = new RVsim.EXE.BarrelShifter.BarrelShifter("Shift", _d.Port, _amount.Port, _right.Port, _arithmetic.Port, _clk.Port);

      _q = new Sink<uint>(_dut.Q);
    }

    [Test]
    public void CheckThatTheFirstStageShiftsBySixteen()
    {
      _d.Set(65536);
      _amount.Set(16);
      _right.Set(true);
      _arithmetic.Set(false);

      Assert.That(_dut.Q.Value, Is.EqualTo(0));

      _clk.Set(true);

      Assert.That(_dut.Q.Value, Is.EqualTo(1));
    }
  }
}
