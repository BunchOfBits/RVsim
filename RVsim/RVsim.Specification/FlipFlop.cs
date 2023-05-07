using NUnit.Framework;

namespace RVsim.Specification
{
  internal class FlipFlop
  {
    private Source _d;
    private Source _clk;
    private RVsim.FlipFlop _dut;
    private Sink _q;
    private Sink _qn;

    [SetUp]
    public void Setup()
    {
      _d = new Source("D");
      _clk = new Source("CLK");

      _dut = new RVsim.FlipFlop("FF1", _d.Port, _clk.Port);

      _q = new Sink(_dut.Q);
      _qn = new Sink(_dut.Qn);
    }

    [Test]
    public void CheckThatFlipFlopHoldsWhenClockIsStable()
    {
      _d.Set(1u);

      Assert.That(_q.Value, Is.EqualTo(0));
      Assert.That(_qn.Value, Is.Not.EqualTo(0));
    }

    [Test]
    public void CheckThatFlipFlopChangesUponPositiveEdgeOfClock()
    {
      _d.Set(1u);
      _clk.Set(1u);

      Assert.That(_q.Value, Is.Not.EqualTo(0));
      Assert.That(_qn.Value, Is.EqualTo(0));
    }

    [Test]
    public void CheckThatFlipFlopIsStabkeAfterPositiveEdgeOfClock()
    {
      _d.Set(1u);
      _clk.Set(1u);
      _d.Set(0u);

      Assert.That(_q.Value, Is.Not.EqualTo(0));
      Assert.That(_qn.Value, Is.EqualTo(0));
    }
  }
}
