using NUnit.Framework;

using PicoSim;

namespace RVsim.Specification
{
  public class AdderTests
  {
    private Source<uint> _a;
    private Source<uint> _b;
    private Adder _dut;
    private Sink<uint> _sink;

    [SetUp]
    public void Setup()
    {
      _a = new Source<uint>("A");
      _b = new Source<uint>("B");

      _dut = new Adder("A1", _a.Port, _b.Port);

      _sink = new Sink<uint>(_dut.Sum);
    }

    [Test]
    public void CheckAdditionWithoutOverflow()
    {
      _a.Set(1u);
      _b.Set(4u);

      Assert.That(_sink.Value, Is.EqualTo(5));
    }
  }
}
