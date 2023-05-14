using NUnit.Framework;

using PicoSim;

using RVsim.Parts;

namespace RVsim.Specification.Parts
{
  internal class FlipFlopTests
  {
    private Source<bool> _d;
    private Source<bool> _clk;
    private FlipFlop _dut;
    private Sink<bool> _q;
    private Sink<bool> _qn;

    [SetUp]
    public void Setup()
    {
      _d = new Source<bool>("D");
      _clk = new Source<bool>("CLK");

      _dut = new FlipFlop("FF1", _d.Port, _clk.Port);

      _q = new Sink<bool>(_dut.Q);
      _qn = new Sink<bool>(_dut.Qn);
    }

    [Test]
    public void CheckThatFlipFlopHoldsWhenClockIsStable()
    {
      _d.Set(true);

      Assert.That(_q.Value, Is.False);
      Assert.That(_qn.Value, Is.True);
    }

    [Test]
    public void CheckThatFlipFlopChangesUponPositiveEdgeOfClock()
    {
      _d.Set(true);
      _clk.Set(true);

      Assert.That(_q.Value, Is.True);
      Assert.That(_qn.Value, Is.False);
    }

    [Test]
    public void CheckThatFlipFlopIsStabkeAfterPositiveEdgeOfClock()
    {
      _d.Set(true);
      _clk.Set(true);
      _d.Set(false);

      Assert.That(_q.Value, Is.True);
      Assert.That(_qn.Value, Is.False);
    }
  }
}
