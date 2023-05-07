using NUnit.Framework;

namespace RVsim.Specification
{
  public class Adder
  {
    private Source _a;
    private Source _b;
    private RVsim.Adder _dut;
    private Sink _sink;

    [SetUp]
    public void Setup()
    {
      _a = new Source("A");
      _b = new Source("B");

      _dut = new RVsim.Adder("A1", _a.Port, _b.Port);

      _sink = new Sink(_dut.Sum);
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
