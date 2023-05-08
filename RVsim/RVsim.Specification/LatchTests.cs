using NUnit.Framework;

using PicoSim;

namespace RVsim.Specification
{
  public class LatchTests
  {
    private Source<bool> _d;
    private Source<bool> _e;
    private Latch _dut;
    private Sink<bool> _q;
    private Sink<bool> _qn;

    [SetUp]
    public void Setup()
    {
      _d = new Source<bool>("D");
      _e = new Source<bool>("E");

      _dut = new Latch("L1", _d.Port, _e.Port);

      _q = new Sink<bool>(_dut.Q);
      _qn = new Sink<bool>(_dut.Qn);
    }

    [Test]
    public void CheckThatLatchHoldsWhenDisabled()
    {
      _d.Set(true);
      _d.Set(false);

      Assert.That(_q.Value, Is.False);
      Assert.That(_qn.Value, Is.True);
    }

    [Test]
    public void CheckThatLatchFollowsWhenEnabled()
    {
      _e.Set(true);
      _d.Set(true);

      Assert.That(_q.Value, Is.True);
      Assert.That(_qn.Value, Is.False);

      _d.Set(false);

      Assert.That(_q.Value, Is.False);
      Assert.That(_qn.Value, Is.True);
    }

    [Test]
    public void CheckThatLatchHoldsLastValueBeforeDisable()
    {
      _e.Set(true);
      _d.Set(true);
      _e.Set(false);
      _d.Set(false);

      Assert.That(_q.Value, Is.True);
      Assert.That(_qn.Value, Is.False);
    }
  }
}
