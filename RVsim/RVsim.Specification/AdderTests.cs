using NUnit.Framework;

using PicoSim;

namespace RVsim.Specification
{
  public class AdderTests
  {
    private Source<uint> _a;
    private Source<uint> _b;
    private Source<bool> _ci;
    private Adder _dut;
    private Sink<uint> _sum;
    private Sink<bool> _co;

    [SetUp]
    public void Setup()
    {
      _a = new Source<uint>("A");
      _b = new Source<uint>("B");
      _ci = new Source<bool>("Ci");

      _dut = new Adder("A1", _a.Port, _b.Port, _ci.Port);

      _sum = new Sink<uint>(_dut.Sum);
      _co = new Sink<bool>(_dut.Co);
    }

    [Test]
    public void CheckAdditionWithoutOverflow()
    {
      _a.Set(1u);
      _b.Set(4u);
      _ci.Set(false);

      Assert.That(_sum.Value, Is.EqualTo(5u));
      Assert.That(_co.Value, Is.False);
    }

    [Test]
    public void CheckAdditionWithOverflow()
    {
      _a.Set(2_000_000_000u);
      _b.Set(4_000_000_000u);
      _ci.Set(false);

      Assert.That(_sum.Value, Is.EqualTo(1_705_032_704u));  // (2E9 + 4E9) mod 2^32
      Assert.That(_co.Value, Is.True);
    }

    [Test]
    public void CheckAdditionWithCarryIn()
    {
      _a.Set(1u);
      _b.Set(4u);
      _ci.Set(true);

      Assert.That(_sum.Value, Is.EqualTo(6u));
      Assert.That(_co.Value, Is.False);
    }
  }
}
